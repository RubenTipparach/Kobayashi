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
	public class HealthLossReductionEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.HealthLossReduction;
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

			if (capitalize)
			{
				toStringBuilder.Append("Reduce the amount of health lost by ");
			}
			else
			{
				toStringBuilder.Append("reduce the amount of health lost by ");
			}

			switch (AmountType)
			{
				case AmountType.Numeric:
					toStringBuilder.Append(Amount.ToString());
					break;
				case AmountType.Variable:
					toStringBuilder.Append(Description.ToDescription(AmountVar));
					break;
				default:
					throw new InvalidOperationException("Unsupported AmountType for HealthLossReductionEffect.");
			}

			return toStringBuilder.ToString();
		}
	}
}
