using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetHackersLib.Cards.Enums
{
	public enum Variable
	{
		X,
		Y,
		Z,

		[Description("X+X")]
		XPlusX,

		[Description("Y+Y")]
		YPlusY,

		[Description("Z+Z")]
		ZPlusZ,

		[Description("X+Y")]
		XPlusY,

		[Description("X+Z")]
		XPlusZ,

		[Description("Y+Z")]
		YPlusZ
	}
}
