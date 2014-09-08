using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetHackers.Cards.Enums
{
	public enum ValueModifier
	{
		None,

		[Description("half {0}, rounded up")]
		HalfRoundedUp,

		[Description("half {0}, rounded down")]
		HalfRoundedDown
	}
}
