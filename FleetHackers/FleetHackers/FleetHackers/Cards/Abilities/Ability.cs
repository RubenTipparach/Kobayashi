using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetHackers.Cards.Abilities
{
	public abstract class Ability
	{
		public abstract AbilityType AbilityType { get; }
		public abstract string ToString(Card card);
	}
}
