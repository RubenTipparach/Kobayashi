using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FleetHackersLib.Cards.Abilities
{
	[DataContract]
	public class AttachToShipAbility : Ability
	{
		public override AbilityType AbilityType
		{
			get
			{
				return AbilityType.AttachToShip;
			}
		}

		public override string ToString(Card card)
		{
			return "Attach to ship";
		}
	}
}
