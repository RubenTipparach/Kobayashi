using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FleetHackers.Cards.Effects;
using System.Runtime.Serialization;
using FleetHackers.Cards.Enums;

namespace FleetHackers.Cards.Abilities
{
	[DataContract]
	public class TriggeredAbility : Ability
	{
		public override AbilityType AbilityType
		{
			get
			{
				return AbilityType.Triggered;
			}
		}

		[DataMember(Name="trigger")]
		public Trigger Trigger { get; set; }

		[DataMember(Name="effect")]
		public Effect Effect { get; set; }

		public override string ToString(Card card)
		{
			if ((card.Subtype & Subtype.Trap) != 0)
			{
				StringBuilder toStringBuilder = new StringBuilder("The next time ");
				toStringBuilder.Append(Trigger.ToString(card));
				toStringBuilder.Append(", ");
				toStringBuilder.Append(Effect.ToString(card));
				toStringBuilder.Append(".");

				return toStringBuilder.ToString();
			}
			else if (card.Supertype != Supertype.Maneuver)
			{
				StringBuilder toStringBuilder = new StringBuilder();
				if (Trigger.TriggerType == TriggerType.EntersTheBattleZone || Trigger.TriggerType == TriggerType.LeavesTheBattleZone)
				{
					toStringBuilder.Append("When ");
				}
				else if (Trigger.TriggerType == TriggerType.Attack || Trigger.TriggerType == TriggerType.Interception)
				{
					toStringBuilder.Append("Whenever ");
				}
				else if (Trigger.TriggerType == TriggerType.Annihilated)
				{
					toStringBuilder.Append("If ");
				}

				toStringBuilder.Append(Trigger.ToString(card));
				toStringBuilder.Append(", ");
				toStringBuilder.Append(Effect.ToString(card));
				toStringBuilder.Append(".");

				return toStringBuilder.ToString();
			}
			else
			{
				throw new InvalidOperationException("Unsupported Card Type for TriggeredAbility.");
			}
		}
	}
}
