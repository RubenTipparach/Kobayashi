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
			return "If " + card.Title + " would inflict enough damage to a ship it attacks to annihilate that ship, you may have " +
				card.Title + " inflict the rest of its damage to another target in its range.";
		}
	}
}
