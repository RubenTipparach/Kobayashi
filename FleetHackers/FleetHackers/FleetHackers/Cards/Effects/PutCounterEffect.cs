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
	public class PutCounterEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.PutCounter;
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
				toStringBuilder.Append("Put ");
			}
			else
			{
				toStringBuilder.Append("put ");
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
				case DivideMethod.YouChooseAny:
					toStringBuilder.Append("divided however you choose on ");
					break;
				case DivideMethod.None:
					toStringBuilder.Append("on ");
					break;
				default:
					throw new InvalidOperationException("Unsupported DivideMethod for PutCounterEffect.");
			}

			if (!TargetsExact)
			{
				toStringBuilder.Append("up to ");
			}

			if (NumTargets == 1)
			{
				toStringBuilder.Append("target ");
			}
			else if(NumTargets > 1)
			{
				toStringBuilder.Append(NumTargets.ToString());
				toStringBuilder.Append(" target ");
			}

			switch (Target)
			{
				case Target.NonInfluence:
					toStringBuilder.Append("non-influence card");
					if (NumTargets > 1)
					{
						toStringBuilder.Append("s");
					}
					break;
				case Target.AttachedShip:
					toStringBuilder.Append("attached ship");
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for PutCounterEffect.");
			}

			return toStringBuilder.ToString();
		}
	}
}
