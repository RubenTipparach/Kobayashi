using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackers.Cards.Effects.Enums;
using System.Collections.ObjectModel;
using FleetHackers.Cards.Enums;

namespace FleetHackers.Cards.Effects
{
	[DataContract]
	public class RemoveCounterEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.RemoveCounter;
			}
		}

		public Target Target { get; set; }

		[DataMember(Name = "target")]
		public string TargetString
		{
			get { return Target.ToString(); }
			set { Target = (Target)Enum.Parse(typeof(Target), value); }
		}

		[DataMember(Name = "numCounters")]
		public int NumCounters { get; set; }

		[DataMember(Name = "countersExact")]
		public bool CountersExact { get; set; }

		[DataMember(Name = "numTargets")]
		public int NumTargets { get; set; }

		[DataMember(Name = "targetsExact")]
		public bool TargetsExact { get; set; }

		public DivideMethod DivideMethod { get; set; }

		[DataMember(Name = "divideMethod")]
		public string DivideMethodString
		{
			get { return DivideMethod.ToString(); }
			set { DivideMethod = (DivideMethod)Enum.Parse(typeof(DivideMethod), value); }
		}

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (capitalize)
			{
				toStringBuilder.Append("Remove ");
			}
			else
			{
				toStringBuilder.Append("remove ");
			}

			if (!CountersExact)
			{
				toStringBuilder.Append("up to ");
			}

			if (NumCounters == 1)
			{
				toStringBuilder.Append("a counter ");
			}
			else
			{
				toStringBuilder.Append(NumCounters.ToString());
				toStringBuilder.Append(" counters ");
			}

			switch (DivideMethod)
			{
				case DivideMethod.None:
					toStringBuilder.Append("from ");
					break;
				default:
					throw new InvalidOperationException("Unsupported DivideMethod for RemoveCounterEffect.");
			}

			if (!TargetsExact)
			{
				toStringBuilder.Append("up to ");
			}

			if (NumTargets > 1)
			{
				toStringBuilder.Append(NumTargets.ToString());
				toStringBuilder.Append(" ");
			}

			switch (Target)
			{
				case Target.NonInfluence:
					toStringBuilder.Append("target non-influence card");
					if (NumTargets > 1)
					{
						toStringBuilder.Append("s");
					}
					break;
				case Target.AttachedShip:
					toStringBuilder.Append("attached ship");
					break;
				case Target.OtherShipYouControl:
					toStringBuilder.Append("another target ship you control");
					break;
				case Target.YourNonInfluence:
					toStringBuilder.Append("target non-influence card you control");
					break;
				case Target.This:
					toStringBuilder.Append(card.Title);
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for RemoveCounterEffect.");
			}

			return toStringBuilder.ToString();
		}
	}
}
