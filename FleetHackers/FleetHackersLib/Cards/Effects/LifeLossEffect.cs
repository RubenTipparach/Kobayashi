using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Reflection;
using FleetHackersLib.Cards.Effects.Enums;
using FleetHackersLib.Cards.Enums;

namespace FleetHackersLib.Cards.Effects
{
	[DataContract]
	public class LifeLossEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.LifeLoss;
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

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

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
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for LifeLossEffect.");
			}

			toStringBuilder.Append("home base loses ");

			switch (AmountType)
			{
				case AmountType.Numeric:
					toStringBuilder.Append(Amount.ToString());
					break;
				case AmountType.Variable:
					toStringBuilder.Append(Description.ToDescription(AmountVar));
					break;
				default:
					throw new InvalidOperationException("Unsupported AmountType for LifeLossEffect.");
			}

			toStringBuilder.Append(" health");

			return toStringBuilder.ToString();
		}
	}
}
