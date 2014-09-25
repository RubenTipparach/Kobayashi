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
	public class RechargeEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.Recharge;
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

		[DataMember(Name = "optional")]
		public bool Optional { get; set; }

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (Optional)
			{
				if (capitalize)
				{
					toStringBuilder.Append("You may recharge ");
				}
				else
				{
					toStringBuilder.Append("you may recharge ");
				}
			}
			else
			{
				if (capitalize)
				{
					toStringBuilder.Append("Recharge ");
				}
				else
				{
					toStringBuilder.Append("recharge ");
				}
			}

			switch (Target)
			{
				case Target.AnyShip:
					toStringBuilder.Append("target ship");
					break;
				case Target.This:
					toStringBuilder.Append(card.Title);
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for RechargeEffect.");
			}

			return toStringBuilder.ToString();
		}
	}
}
