using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetHackers.Cards.Enums
{
	public enum ActivePlayerAttribute
	{
		None,

		[Description("you control {0} ship{1}")]
		NumShipsYouControl,

		[Description("an opponent has {0} card{1} in his or her junkyard")]
		OpponentJunkyardCount,

		[Description("an opponent controls {0} device{1}")]
		NumDevicesOpponentControls,

		[Description("you have played {0} other non-influence card{1} this turn")]
		OtherNonInfluenceCardsPlayedThisTurn
	}
}
