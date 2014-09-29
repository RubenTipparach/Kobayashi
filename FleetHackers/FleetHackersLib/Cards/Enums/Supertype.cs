using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetHackers.Cards.Enums
{
	[Flags]
	public enum Supertype
	{
		None = 0,
		Ship = 1,
		Maneuver = 2,
		Device = 4,
		Influence = 8,
		Token = 16
	}
}
