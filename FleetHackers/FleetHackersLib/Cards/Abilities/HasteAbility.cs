using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackersLib.Cards.Effects;
using System.Collections.ObjectModel;

namespace FleetHackersLib.Cards.Abilities
{
	[DataContract]
	public class HasteAbility : Ability
	{
		public override AbilityType AbilityType
		{
			get
			{
				return AbilityType.Haste;
			}
		}

		public override string ToString(Card card)
		{
			return card.Title + " can attack and move the turn it comes under your control in the battle zone.";
		}
	}
}
