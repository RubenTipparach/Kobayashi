using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetHackersLib.Cards.Enums
{
	public enum ValueModifier
	{
		[Description("{0}")]
		None,

		[Description("half {0}, rounded up")]
		HalfRoundedUp,

		[Description("half {0}, rounded down")]
		HalfRoundedDown,

		[Description("twice {0}")]
		Twice
	}
}
