using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackersLib.Cards.Effects.Conditions;
using FleetHackersLib.Cards.Enums;
using FleetHackersLib.Cards.Effects.Enums;

namespace FleetHackersLib.Cards.Effects
{
	[DataContract]
	public class FreePlayEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.FreePlay;
			}
		}

		[DataMember(Name = "condition")]
		public EffectCondition Condition { get; set; }

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
				toStringBuilder.Append("You ");
			}
			else
			{
				toStringBuilder.Append("you ");
			}

			toStringBuilder.Append("may play ");

			switch (Target)
			{
				case Target.NonInfluence:
					toStringBuilder.Append("a non-influence card ");
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for FreePlayEffect.");
			}

			toStringBuilder.Append(Condition.ToString());
			toStringBuilder.Append("without paying its energy costs");

			return toStringBuilder.ToString();
		}

		public override bool HasReminderText { get { return true; } }
		public override string ReminderText { get { return "(You still need to satisfy its influence requirements and any other additional costs.)"; } }
	}
}
