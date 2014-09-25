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
	public class CloakAbility : Ability
	{
		public override AbilityType AbilityType
		{
			get
			{
				return AbilityType.Cloak;
			}
		}

		public override string ToString(Card card)
		{
			return "Cloak";
		}
	}
}
