using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackers.Cards.Effects.Enums;
using FleetHackers.Cards.Enums;

namespace FleetHackers.Cards.Effects
{
	[DataContract]
	public class StaticCostReductionEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.StaticCostReduction;
			}
		}

		public Supertype CardType { get; set; }

		[DataMember(Name = "cardType")]
		public string CardTypeString
		{
			get
			{
				return CardType.ToString();
			}
			set
			{
				CardType = (Supertype)Enum.Parse(typeof(Supertype), value);
			}
		}

		[DataMember(Name = "reductionAmount")]
		public int ReductionAmount { get; set; }

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (capitalize)
			{
				toStringBuilder.Append("Energy costs of ");
			}
			else
			{
				toStringBuilder.Append("energy costs of ");
			}

			switch (CardType)
			{
				case Supertype.Maneuver:
					toStringBuilder.Append("maneuvers you initiate ");
					break;
				default:
					throw new InvalidOperationException("Unsupported CardType for StaticCostReductionEffect.");
			}

			toStringBuilder.Append("are reduced by {");
			toStringBuilder.Append(ReductionAmount.ToString());
			toStringBuilder.Append("}, to a minimum of {1}");

			return toStringBuilder.ToString();
		}

		public override bool HasReminderText { get { return true; } }

		public override string ReminderText
		{
			get
			{
				return "(Maneuvers with energy cost {0} still cost {0}.)";
			}
		}
	}
}
