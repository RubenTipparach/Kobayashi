using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using System.Reflection;
using FleetHackers.Cards.Effects.Enums;

namespace FleetHackers.Cards.Effects
{
	[DataContract]
	public class StatPumpEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.StatPump;
			}
		}

		public Target Target { get; set; }

		[DataMember(Name = "target")]
		public string TargetString
		{
			get { return Target.ToString(); }
			set { Target = (Target)Enum.Parse(typeof(Target), value); }
		}

		[DataMember(Name = "attackPump")]
		public int AttackPump { get; set; }

		[DataMember(Name = "defensePump")]
		public int DefensePump { get; set; }

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
					throw new InvalidOperationException("Unsupported Target for StatPumpEffect.");
			}

			toStringBuilder.Append("gets ");
			if (AttackPump > 0)
			{
				toStringBuilder.Append("+");
			}
			else if(AttackPump == 0)
			{
				if (DefensePump < 0)
				{
					toStringBuilder.Append("-");
				}
				else
				{
					toStringBuilder.Append("+");
				}
			}
			toStringBuilder.Append(AttackPump.ToString());
			toStringBuilder.Append("/");
			if (DefensePump > 0)
			{
				toStringBuilder.Append("+");
			}
			else if (DefensePump == 0)
			{
				if (AttackPump < 0)
				{
					toStringBuilder.Append("-");
				}
				else
				{
					toStringBuilder.Append("+");
				}
			}
			toStringBuilder.Append(DefensePump.ToString());

			switch (EffectEnds)
			{
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
