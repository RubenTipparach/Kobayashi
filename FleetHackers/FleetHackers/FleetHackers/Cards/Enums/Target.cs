using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetHackers.Cards.Enums
{
	public enum Target
	{
		None,
		YourShip,
		YourHomeBase,
		OpponentShip,
		OpponentHomeBase,
		AttackingShips,
		This,
		AnyShip,
		UpTo2OtherShips,
		AnyDevice,
		OtherDevice,
		InterceptingShip,
		NonInfluence,
		YourChargedShips,
		Opponent,
		AttachedShip,
		TargettedShip
	}
}
