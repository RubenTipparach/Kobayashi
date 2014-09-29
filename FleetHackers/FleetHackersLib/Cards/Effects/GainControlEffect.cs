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
	public class GainControlEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.GainControl;
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

			if (capitalize)
			{
				toStringBuilder.Append("You gain control of ");
			}
			else
			{
				toStringBuilder.Append("you gain control of ");
			}

			switch (Target)
			{
				case Target.ChosenDevice:
					toStringBuilder.Append("the chosen device");
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for GainControlEffect.");
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
					throw new InvalidOperationException("Unsupported EffectEnds for GainControlEffect.");
			}

			return toStringBuilder.ToString();
		}
	}
}
