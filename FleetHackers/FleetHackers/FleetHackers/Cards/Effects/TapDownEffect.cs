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
	public class TapDownEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.TapDown;
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

			switch (Target)
			{
				case Target.AttachedShip:
					if (capitalize)
					{
						toStringBuilder.Append("The attached ship ");
					}
					else
					{
						toStringBuilder.Append("the attached ship ");
					}
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for TapDownEffect.");
			}

			toStringBuilder.Append("does not recharge during its controller's recharge step");

			return toStringBuilder.ToString();
		}
	}
}
