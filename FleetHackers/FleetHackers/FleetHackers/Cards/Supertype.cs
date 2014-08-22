using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetHackers.Cards
{
	[Flags]
	public enum Supertype
	{
		Ship,
		Maneuver,
		Device,
		Influence,
		Token
	}
}
