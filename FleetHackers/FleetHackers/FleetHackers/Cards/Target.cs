using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetHackers.Cards
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
		OtherDevice
	}
}
