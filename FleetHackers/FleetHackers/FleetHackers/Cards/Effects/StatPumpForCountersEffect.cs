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
	public class StatPumpForCountersEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.StatPumpForCounters;
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

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			switch (Target)
			{
				case Target.AttachedShip:
					if (capitalize)
					{
						toStringBuilder.Append("Attached ship");
					}
					else
					{
						toStringBuilder.Append("attached ship");
					}
					break;
				case Target.This:
					toStringBuilder.Append(card.Title);
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for StatPumpForCountersEffect.");
			}

			toStringBuilder.Append(" gets ");
			if (AttackPump > 0)
			{
				toStringBuilder.Append("+");
			}
			else if (AttackPump == 0)
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

			toStringBuilder.Append(" for each counter on it.");

			return toStringBuilder.ToString();
		}
	}
}
