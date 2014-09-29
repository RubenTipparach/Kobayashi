using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetHackersLib.Cards.Abilities
{
	public enum TriggerType
	{
		Attack,
		Interception,
		EntersTheBattleZone,
		EndOfYourTurn,
		LeavesTheBattleZone,
		Annihilated,
		LifeLoss,
		Damage,
		AssaultDamage
	}
}
