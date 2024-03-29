﻿using System;
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
	public class RangePumpEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.RangePump;
			}
		}

		public Target Target { get; set; }

		[DataMember(Name = "target")]
		public string TargetString
		{
			get { return Target.ToString(); }
			set { Target = (Target)Enum.Parse(typeof(Target), value); }
		}

		[DataMember(Name = "rangePump")]
		public int RangePump { get; set; }

		public PointInTime EffectEnds { get; set; }

		[DataMember(Name = "effectEnds")]
		public string EffectEndsString
		{
			get { return EffectEnds.ToString(); }
			set { EffectEnds = (PointInTime)Enum.Parse(typeof(PointInTime), value); }
		}

		public Subtype Subtype { get; set; }

		[DataMember(Name = "subtype")]
		public string SubtypeString
		{
			get
			{
				return Subtype.ToString();
			}
			set
			{
				Subtype = (Subtype)Enum.Parse(typeof(Subtype), value);
			}
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
				case Target.AttachedShip:
					toStringBuilder.Append(capitalize ? "Attached ship " : "attached ship ");
					break;
				case Target.OtherShipsOfType:
					toStringBuilder.Append(capitalize ? "Other " : "other ");
					toStringBuilder.Append(Subtype.ToString());
					toStringBuilder.Append(" ships ");
					break;
				case Target.InterceptingShip:
					toStringBuilder.Append(capitalize ? "The intercepting ship " : "the intercepting ship ");
					break;
				case Target.InterceptedShip:
					toStringBuilder.Append(capitalize ? "The intercepted ship " : "the intercepted ship ");
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for RangePumpEffect.");
			}

			if (RangePump < 0)
			{
				toStringBuilder.Append("loses ");
			}
			else
			{
				toStringBuilder.Append("gains ");
			}

			toStringBuilder.Append(Math.Abs(RangePump).ToString());
			toStringBuilder.Append(" range");

			switch (EffectEnds)
			{
				case PointInTime.None:
					break;
				case PointInTime.EndOfTurn:
					toStringBuilder.Append(" until end of turn");
					break;
				case PointInTime.BeginningOfYourTurn:
					toStringBuilder.Append(" until the beginning of your next turn");
					break;
				default:
					throw new InvalidOperationException("Unsupported EffectEnds for StatPumpEffect.");
			}

			return toStringBuilder.ToString();
		}
	}
}
