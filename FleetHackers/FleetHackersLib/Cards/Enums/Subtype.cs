using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetHackersLib.Cards.Enums
{
	[Flags]
	public enum Subtype
	{
		None = 0,
		Fighter = 1,
		Trap = 2,
		Trooper = 4,
		Command = 8,
		Dreadnought = 16,
		Kamikaze = 32,
		Shadow = 64,
		Mentat = 128,
		Enabler = 256,
		Upgrade = 512,
		Manipulator = 1024,
		Lawkeeper = 2048,
		Hani = 4096,
		Minion = 8192,
		Mercenary = 16384,
		Trader = 32768,
		Flagship = 65536,
		Wingship = 131072,
		Pirate = 262144,
		Destroyer = 524288,
		Budoka = 1048576,
		Privateer = 2097152,
		Sentinel = 4194304,
		Criminal = 8388608,
		Defender = 16777216,
		Tinker = 33554432,
		Whyr = 67108864,
		Megadrone = 134217728,
		Science = 268435456,
		Drone = 536870912
	}
}
