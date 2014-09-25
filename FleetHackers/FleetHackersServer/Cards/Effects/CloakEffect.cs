using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using System.Reflection;
using FleetHackers.Cards.Effects.Enums;
using FleetHackers.Cards.Enums;

namespace FleetHackers.Cards.Effects
{
	[DataContract]
	public class CloakEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.Cloak;
			}
		}

		public Target Target { get; set; }

		[DataMember(Name = "target")]
		public string TargetString
		{
			get { return Target.ToString(); }
			set { Target = (Target)Enum.Parse(typeof(Target), value); }
		}

		public PointInTime EffectEnds { get; set; }

		[DataMember(Name = "effectEnds")]
		public string EffectEndsString
		{
			get { return EffectEnds.ToString(); }
			set { EffectEnds = (PointInTime)Enum.Parse(typeof(PointInTime), value); }
		}

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			switch (Target)
			{
				case Target.This:
					toStringBuilder.Append(card.Title);
					toStringBuilder.Append(" ");
					break;
				case Target.AnyShip:
					toStringBuilder.Append(capitalize ? "Target ship " : "target ship ");
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for CloakEffect.");
			}

			toStringBuilder.Append("gains cloak");

			switch (EffectEnds)
			{
				case PointInTime.EndOfTurn:
					toStringBuilder.Append(" until end of turn");
					break;
				case PointInTime.BeginningOfYourTurn:
					toStringBuilder.Append(" until the beginning of your next turn");
					break;
				default:
					throw new InvalidOperationException("Unsupported EffectEnds for CloakEffect.");
			}

			return toStringBuilder.ToString();
		}
	}
}
