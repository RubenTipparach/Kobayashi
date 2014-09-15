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
		NumShipsYouControl,

		[Description("an opponent has {0} cards in his or her junkyard")]
		OpponentJunkyardCount
	}
}
