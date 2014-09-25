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
	public class FogEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.Fog;
			}
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
				toStringBuilder.Append("Until ");
			}
			else
			{
				toStringBuilder.Append("until ");
			}

			switch (EffectEnds)
			{
				case PointInTime.EndOfTurn:
					toStringBuilder.Append("end of turn, ");
					break;
				case PointInTime.BeginningOfYourTurn:
					toStringBuilder.Append("the beginning of your next turn, ");
					break;
				default:
					throw new InvalidOperationException("Unsupported EffectEnds for FogEffect.");
			}

			toStringBuilder.Append("ships can't be annihilated and damage can't be inflicted");

			return toStringBuilder.ToString();
		}
	}
}
