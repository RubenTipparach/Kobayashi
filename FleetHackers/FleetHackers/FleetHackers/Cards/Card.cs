using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using FleetHackers.Cards.Abilities;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;
using FleetHackers.Cards.Enums;
using FleetHackers.Cards.AlternateCosts;

namespace FleetHackers.Cards
{
	[DataContract]
	public class Card
	{
		[DataMember(Name="title")]
		public string Title { get; set; }

		[DataMember(Name="energyCost")]
		public int EnergyCost { get; set; }

		public AmountType EnergyCostType { get; set; }

		[DataMember(Name="energyCostType")]
		public string EnergyCostTypeString
		{
			get { return EnergyCostType.ToString(); }
			set { EnergyCostType = (AmountType)Enum.Parse(typeof(AmountType), value); }
		}

		public Variable EnergyCostVar { get; set; }

		[DataMember(Name = "energyCostVar")]
		public string EnergyCostVarString
		{
			get { return EnergyCostVar.ToString(); }
			set { EnergyCostVar = (Variable)Enum.Parse(typeof(Variable), value); }
		}

		public Alignment Alignment { get; set; }

		[DataMember(Name = "alignment")]
		public string AlignmentString
		{
			get { return Alignment.ToString(); }
			set { Alignment = (Alignment)Enum.Parse(typeof(Alignment), value); }
		}

		[DataMember(Name="influenceRequirement")]
		public int InfluenceRequirement { get; set; }

		public Supertype Supertype { get; set; }

		[DataMember(Name = "supertype")]
		public string SupertypeString
		{
			get { return Supertype.ToString(); }
			set { Supertype = (Supertype)Enum.Parse(typeof(Supertype), value); }
		}

		public Subtype Subtype { get; set; }

		[DataMember(Name = "subtype")]
		public string SubtypeString
		{
			get { return Subtype.ToString(); }
			set { Subtype = (Subtype)Enum.Parse(typeof(Subtype), value); }
		}

		[DataMember(Name="range")]
		public int Range { get; set; }

		[DataMember(Name="attack")]
		public int Attack { get; set; }

		[DataMember(Name="defense")]
		public int Defense { get; set; }

		[DataMember(Name="additionalCost")]
		public AlternateCost AdditionalCost { get; set; }

		[DataMember(Name="abilities")]
		private readonly List<Ability> _abilities = new List<Ability>();
		private ReadOnlyCollection<Ability> _abilitiesView;
		public ReadOnlyCollection<Ability> Abilities
		{
			get
			{
				if (_abilitiesView == null)
				{
					_abilitiesView = new ReadOnlyCollection<Ability>(_abilities);
				}
				return _abilitiesView;
			}
		}

		private string _rulesText;
		public string RulesText
		{
			get
			{
				if (_rulesText == null)
				{
					_rulesText = GenerateRulesText();
				}
				return _rulesText;
			}
		}

		private string GenerateRulesText()
		{
			List<string> rulesTextStrings = new List<string>();

			if (AdditionalCost != null)
			{
				StringBuilder additionalCostBuilder = new StringBuilder("As an additional cost to ");

				if ((Supertype & Supertype.Maneuver) > 0)
				{
					additionalCostBuilder.Append("initiate ");
				}
				else
				{
					additionalCostBuilder.Append("deploy ");
				}

				additionalCostBuilder.Append(Title);
				additionalCostBuilder.Append(", ");
				additionalCostBuilder.Append(AdditionalCost.ToString(this));
				additionalCostBuilder.Append(".");
				rulesTextStrings.Add(additionalCostBuilder.ToString());
			}

			if (_abilities != null)
			{
				foreach (Ability ability in _abilities)
				{
					rulesTextStrings.Add(ability.ToString(this));
				}
			}

			return string.Join(Environment.NewLine, rulesTextStrings);
		}
	}
}
