using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Reflection;
using FleetHackers.Cards.Effects.Enums;
using FleetHackers.Cards.Enums;

namespace FleetHackers.Cards.Effects
{
	[DataContract]
	public class LifeGainEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.LifeGain;
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

		[DataMember(Name = "amount")]
		public int Amount { get; set; }

		public AmountType AmountType { get; set; }

		[DataMember(Name = "amountType")]
		public string AmountTypeString
		{
			get { return AmountType.ToString(); }
			set { AmountType = (AmountType)Enum.Parse(typeof(AmountType), value); }
		}

		public Variable AmountVar { get; set; }

		[DataMember(Name = "amountVar")]
		public string AmountVarString
		{
			get { return AmountVar.ToString(); }
			set { AmountVar = (Variable)Enum.Parse(typeof(Variable), value); }
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

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			string targetOwns = string.Empty;
			switch (Target)
			{
				case Target.Opponent:
					if (capitalize)
					{
						toStringBuilder.Append("Target opponent's ");
					}
					else
					{
						toStringBuilder.Append("target opponent's ");
					}
					targetOwns = "that player's";
					break;
				case Target.You:
					if (capitalize)
					{
						toStringBuilder.Append("Your ");
					}
					else
					{
						toStringBuilder.Append("your ");
					}
					targetOwns = "your";
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for LifeGainEffect.");
			}

			toStringBuilder.Append("home base gains ");

			switch (AmountType)
			{
				case AmountType.Numeric:
					toStringBuilder.Append(Amount.ToString());
					break;
				case AmountType.Variable:
					toStringBuilder.Append(Description.ToDescription(AmountVar));
					break;
				default:
					throw new InvalidOperationException("Unsupported AmountType for LifeGainEffect.");
			}

			toStringBuilder.Append(" health");

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
