using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using System.Reflection;
using FleetHackersLib.Cards.Effects.Enums;
using FleetHackersLib.Cards.Enums;

namespace FleetHackersLib.Cards.Effects
{
	[DataContract]
	public class UnblockableEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.Unblockable;
			}
		}

		public Target Target { get; set; }

		[DataMember(Name = "target")]
		public string TargetString
		{
			get { return Target.ToString(); }
			set { Target = (Target)Enum.Parse(typeof(Target), value); }
		}

		public Target Subject { get; set; }

		[DataMember(Name = "subject")]
		public string SubjectString
		{
			get { return Subject.ToString(); }
			set { Subject = (Target)Enum.Parse(typeof(Target), value); }
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
					throw new InvalidOperationException("Unsupported Target for UnblockableEffect.");
			}

			toStringBuilder.Append("can't be damaged by defending ships");

			switch (Subject)
			{
				case Target.None:
					break;
				case Target.ThatPlayer:
					toStringBuilder.Append(" that player controls");
					break;
				default:
					throw new InvalidOperationException("Unsupported Subject for UnblockableEffect.");
			}

			switch (EffectEnds)
			{
				case PointInTime.EndOfTurn:
					toStringBuilder.Append(" until end of turn");
					break;
				case PointInTime.BeginningOfYourTurn:
					toStringBuilder.Append(" until the beginning of your next turn");
					break;
				case PointInTime.None:
					break;
				default:
					throw new InvalidOperationException("Unsupported EffectEnds for UnblockableEffect.");
			}

			return toStringBuilder.ToString();
		}
	}
}
