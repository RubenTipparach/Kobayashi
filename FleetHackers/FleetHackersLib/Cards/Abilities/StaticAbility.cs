﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackersLib.Cards.AlternateCosts;
using FleetHackersLib.Cards.Effects;
using FleetHackersLib.Cards.Effects.Conditions;

namespace FleetHackersLib.Cards.Abilities
{
	[DataContract]
	public class StaticAbility : Ability
	{
		public override AbilityType AbilityType
		{
			get
			{
				return AbilityType.Static;
			}
		}

		[DataMember(Name = "condition")]
		public EffectCondition Condition { get; set; }

		[DataMember(Name = "effect")]
		public Effect Effect { get; set; }

		public override string ToString(Card card)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (Condition != null)
			{
				toStringBuilder.Append("As long as ");
				toStringBuilder.Append(Condition.ToString());
				toStringBuilder.Append(", ");
			}
			toStringBuilder.Append(Effect.ToString(card, Condition == null));
			toStringBuilder.Append(".");

			if (Effect.HasReminderText)
			{
				toStringBuilder.Append(" " + Effect.ReminderText);
			}

			return toStringBuilder.ToString();
		}
	}
}
