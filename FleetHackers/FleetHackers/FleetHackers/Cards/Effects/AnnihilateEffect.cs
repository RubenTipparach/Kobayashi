using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using System.Reflection;
using FleetHackers.Cards.Effects.Enums;
using FleetHackers.Cards.Enums;
using FleetHackers.Cards.Effects.Conditions;

namespace FleetHackers.Cards.Effects
{
	[DataContract]
	public class AnnihilateEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.Annihilate;
			}
		}

		public Target Target { get; set; }

		[DataMember(Name = "target")]
		public string TargetString
		{
			get
			{
				return Target.ToString();
			}
			set
			{
				Target = (Target)Enum.Parse(typeof(Target), value);
			}
		}

		[DataMember(Name = "varDefinitions")]
		private readonly List<VariableDefinition> _varDefinitions = new List<VariableDefinition>();
		private ReadOnlyCollection<VariableDefinition> _varDefinitionsView;
		public ReadOnlyCollection<VariableDefinition> VarDefinitions
		{
			get
			{
				if (_varDefinitionsView == null)
				{
					_varDefinitionsView = new ReadOnlyCollection<VariableDefinition>(_varDefinitions);
				}
				return _varDefinitionsView;
			}
		}

		[DataMember(Name = "optional")]
		public bool Optional { get; set; }

		[DataMember(Name = "condition")]
		public EffectCondition Condition { get; set; }

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (Optional)
			{
				if (capitalize)
				{
					toStringBuilder.Append("You may annihilate ");
				}
				else
				{
					toStringBuilder.Append("you may annihilate ");
				}
			}
			else
			{
				if (capitalize)
				{
					toStringBuilder.Append("Annihilate ");
				}
				else
				{
					toStringBuilder.Append("annihilate ");
				}
			}

			string targetOwns = string.Empty;
			switch (Target)
			{
				case Target.AnyDevice:
					toStringBuilder.Append("target device");
					targetOwns = "that device's";
					break;
				case Target.OtherDevice:
					toStringBuilder.Append("another target device");
					targetOwns = "that device's";
					break;
				case Target.AnyShip:
					toStringBuilder.Append("target ship");
					targetOwns = "that ship's";
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for AnnihilateEffect.");
			}

			if (Condition != null)
			{
				toStringBuilder.Append(" ");
				toStringBuilder.Append(Condition.ToString());
			}

			if (_varDefinitions != null)
			{
				bool firstDef = true;
				foreach (VariableDefinition def in _varDefinitions)
				{
					if (firstDef)
					{
						toStringBuilder.Append(", where ");

						firstDef = false;
					}
					else
					{
						toStringBuilder.Append(" and ");
					}

					toStringBuilder.Append(Description.ToDescription(def.Variable));
					toStringBuilder.Append(" is ");

					switch (def.ValueType)
					{
						case AmountType.Attribute:
							toStringBuilder.Append(string.Format(Description.ToDescription(def.ValueAttribute), targetOwns));
							break;
						default:
							throw new InvalidOperationException("Unsupported AmountType for VariableDefinition.");
					}
				}
			}

			return toStringBuilder.ToString();
		}
	}
}
