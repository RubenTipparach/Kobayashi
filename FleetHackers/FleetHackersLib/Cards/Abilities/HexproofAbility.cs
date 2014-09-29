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
	public class HexproofAbility : Ability
	{
		public override AbilityType AbilityType
		{
			get
			{
				return AbilityType.Hexproof;
			}
		}

		public override string ToString(Card card)
		{
			return "Abilities and maneuvers controlled by your opponents can't target " + card.Title + ".";
		}
	}
}
