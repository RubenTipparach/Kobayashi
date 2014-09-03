using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetHackers.Cards.Enums
{
	public enum ActivePlayerAttribute
	{
		None,

		[Description("you control {0} ships")]
		NumShipsYouControl
	}
}
