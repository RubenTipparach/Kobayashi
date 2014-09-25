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
	public class AttachToShipEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.AttachToShip;
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
				toStringBuilder.Append("Attach ");
			}
			else
			{
				toStringBuilder.Append("attach ");
			}

			toStringBuilder.Append(card.Title);
			toStringBuilder.Append(" to ");

			switch (Target)
			{
				case Target.TargettedShip:
					toStringBuilder.Append("that ship");
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for AttachToShipEffect.");
			}

			return toStringBuilder.ToString();
		}
	}
}
