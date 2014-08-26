using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackers.Cards.Effects.Enums;

namespace FleetHackers.Cards.Effects
{
	[DataContract]
	public class ExhaustEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.Exhaust;
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
					toStringBuilder.Append("You may exhaust ");
				}
				else
				{
					toStringBuilder.Append("you may exhaust ");
				}
			}
			else
			{
				if (capitalize)
				{
					toStringBuilder.Append("Exhaust ");
				}
				else
				{
					toStringBuilder.Append("exhaust ");
				}
			}

			switch (Target)
			{
				case Target.AnyShip:
					toStringBuilder.Append("target ship");
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for ExhaustEffect.");
			}

			return toStringBuilder.ToString();
		}
	}
}
