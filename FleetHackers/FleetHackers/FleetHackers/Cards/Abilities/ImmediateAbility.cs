using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackers.Cards.Effects;

namespace FleetHackers.Cards.Abilities
{
	[DataContract]
	public class ImmediateAbility : Ability
	{
		public override AbilityType AbilityType
		{
			get
			{
				return AbilityType.Immediate;
			}
		}

		[DataMember(Name = "effect")]
		public Effect Effect { get; set; }

		public override string ToString(Card card)
		{
			if (card.Supertype == Supertype.Maneuver)
			{
				StringBuilder toStringBuilder = new StringBuilder(Effect.ToString(card, true));
				toStringBuilder.Append(".");

				return toStringBuilder.ToString();
			}
			else
			{
				throw new InvalidOperationException("Unsupported Card Type for ImmediateAbility.");
			}
		}
	}
}
