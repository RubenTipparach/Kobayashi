using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FleetHackers.Cards.Abilities
{
	[DataContract]
	public class EntersWithCountersAbility: Ability
	{
		public override AbilityType AbilityType
		{
			get
			{
				return AbilityType.EntersWithCounters;
			}
		}

		[DataMember(Name = "numCounters")]
		public int NumCounters { get; set; }

		public override string ToString(Card card)
		{
			StringBuilder toStringBuilder = new StringBuilder(card.Title);

			toStringBuilder.Append(" enters the battle zone with ");
			toStringBuilder.Append(NumCounters.ToString());
			toStringBuilder.Append(" counters on it.");

			return toStringBuilder.ToString();
		}
	}
}
