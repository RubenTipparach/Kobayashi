using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FleetHackersLib.Cards.Abilities
{
	[DataContract]
	public class SecretTechAbility : Ability
	{
		public override AbilityType AbilityType
		{
			get
			{
				return AbilityType.SecretTech;
			}
		}

		public override string ToString(Card card)
		{
			return "Secret Tech";
		}
	}
}
