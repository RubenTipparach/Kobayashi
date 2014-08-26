using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackers.Cards.Effects.Enums;

namespace FleetHackers.Cards.Effects
{
	[DataContract]
	public class ForfeitEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.Forfeit;
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

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (capitalize)
			{
				toStringBuilder.Append("Forfeit ");
			}
			else
			{
				toStringBuilder.Append("forfeit ");
			}

			switch (Target)
			{
				case Target.This:
					toStringBuilder.Append(card.Title);
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for ForfeitEffect.");
			}

			return toStringBuilder.ToString();
		}
	}
}
