using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FleetHackers.Cards.Effects;

namespace FleetHackers.Cards.Abilities
{
	public class TriggeredAbility : Ability
	{
		public override AbilityType AbilityType
		{
			get
			{
				return AbilityType.Trigger;
			}
		}

		public Trigger Trigger { get; set; }
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
			else
			{
				throw new InvalidOperationException("Unsupported Card Subtype for TriggeredAbility.");
			}
		}
	}
}
