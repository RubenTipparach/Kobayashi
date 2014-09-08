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

		[DataMember(Name = "energyCost")]
		public int EnergyCost { get; set; }

		[DataMember(Name = "additionalCost")]
		public AlternateCost AdditionalCost { get; set; }

		[DataMember(Name = "effect")]
		public Effect Effect { get; set; }

		public override string ToString(Card card)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if ((EnergyCost > 0) || (AdditionalCost == null))
			{
				toStringBuilder.Append("{");
				toStringBuilder.Append(EnergyCost.ToString());
				toStringBuilder.Append("}");
				if (AdditionalCost != null)
				{
					toStringBuilder.Append(", ");
				}
			}
			if (AdditionalCost != null)
			{
				toStringBuilder.Append(AdditionalCost.ToString(card, true));
			}
			toStringBuilder.Append(": ");
			toStringBuilder.Append(Effect.ToString(card, true));
			toStringBuilder.Append(".");

			return toStringBuilder.ToString();
		}
	}
}
