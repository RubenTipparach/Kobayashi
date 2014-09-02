using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackers.Cards.AlternateCosts;
using FleetHackers.Cards.Effects;

namespace FleetHackers.Cards.Abilities
{
	[DataContract]
	public class ActivatedAbility : Ability
	{
		public override AbilityType AbilityType
		{
			get
			{
				return AbilityType.Activated;
			}
		}

		[DataMember(Name = "cost")]
		public AlternateCost Cost { get; set; }

		[DataMember(Name = "effect")]
		public Effect Effect { get; set; }

		public override string ToString(Card card)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			toStringBuilder.Append(Cost.ToString(card, true));
			toStringBuilder.Append(": ");
			toStringBuilder.Append(Effect.ToString(card, true));

			return toStringBuilder.ToString();
		}
	}
}
