﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackersLib.Cards.Effects;
using System.Collections.ObjectModel;

namespace FleetHackersLib.Cards.Abilities
{
	[DataContract]
	public class SingleAttackAbility : Ability
	{
		public override AbilityType AbilityType
		{
			get
			{
				return AbilityType.SingleAttack;
			}
		}

		public override string ToString(Card card)
		{
			return card.Title + " can only attack once per turn.";
		}
	}
}
