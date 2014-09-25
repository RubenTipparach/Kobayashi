using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackers.Cards.AlternateCosts;
using FleetHackers.Cards.Effects;
using System.Collections.ObjectModel;

namespace FleetHackers.Cards.Abilities
{
	[DataContract]
	public class ActivatedAbility : Ability
	{
		public override AbilityType AbilityType
		{
			get
			{
				return AbilityType.Activated;
			}
		}

		[DataMember(Name = "energyCost")]
		public int EnergyCost { get; set; }

		[DataMember(Name = "additionalCosts")]
		private readonly List<AlternateCost> _additionalCosts = new List<AlternateCost>();
		private ReadOnlyCollection<AlternateCost> _additionalCostsView;
		public ReadOnlyCollection<AlternateCost> AdditionalCosts
		{
			get
			{
				if (_additionalCosts == null)
				{
					return null;
				}
				if (_additionalCostsView == null)
				{
					_additionalCostsView = new ReadOnlyCollection<AlternateCost>(_additionalCosts);
				}
				return _additionalCostsView;
			}
		}

		[DataMember(Name = "effect")]
		public Effect Effect { get; set; }

		public override string ToString(Card card)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if ((EnergyCost > 0) || (_additionalCosts == null) || (_additionalCosts.Count == 0))
			{
				toStringBuilder.Append("{");
				toStringBuilder.Append(EnergyCost.ToString());
				toStringBuilder.Append("}");
				if ((_additionalCosts != null) && (_additionalCosts.Count > 0))
				{
					toStringBuilder.Append(", ");
				}
			}
			if (_additionalCosts != null)
			{
				List<string> additionalCostStrings = new List<string>();
				foreach (AlternateCost cost in _additionalCosts)
				{
					additionalCostStrings.Add(cost.ToString(card, true));
				}
				toStringBuilder.Append(string.Join(", ", additionalCostStrings));
			}
			toStringBuilder.Append(": ");
			toStringBuilder.Append(Effect.ToString(card, true));
			toStringBuilder.Append(".");

			return toStringBuilder.ToString();
		}
	}
}
