using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FleetHackers.Cards.Effects.Enums;
using System.Runtime.Serialization;
using FleetHackers.Cards.Enums;

namespace FleetHackers.Cards.Effects
{
	[DataContract]
	public class RestoreEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.Restore;
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
				toStringBuilder.Append("Return ");
			}
			else
			{
				toStringBuilder.Append("return ");
			}

			switch (Target)
			{
				case Target.AnyShip:
					toStringBuilder.Append("target ship");
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for RestoreEffect.");
			}

			toStringBuilder.Append(" from your junkyard to your hand");

			return toStringBuilder.ToString();
		}
	}
}
