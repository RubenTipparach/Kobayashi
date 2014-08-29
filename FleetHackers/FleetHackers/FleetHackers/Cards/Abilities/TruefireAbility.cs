using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackers.Cards.Effects;
using System.Collections.ObjectModel;

namespace FleetHackers.Cards.Abilities
{
	[DataContract]
	public class TruefireAbility : Ability
	{
		public override AbilityType AbilityType
		{
			get
			{
				return AbilityType.Truefire;
			}
		}

		public override string ToString(Card card)
		{
			return "When " + card.Title + "attacks, you may choose to assign its battle damage as though it wasn’t intercepted.";
		}
	}
}
