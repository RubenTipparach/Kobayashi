using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetHackersLib.Cards.Enums
{
	public enum CardAttribute
	{
		None,

		[Description("attack")]
		Attack,

		[Description("defense")]
		Defense,

		[Description("the number of influence cards controlled by {0} controller")]
		ControllersInfluence,

		[Description("energy cost")]
		EnergyCost,

		[Description("the combined defense of any opposing ships")]
		OpposingShipDefense,

		[Description("the number of cards in {0} controller's junkyard")]
		ConrollersJunkyardCount,

		[Description("the number of other {1} ships in the battle zone")]
		OtherShipsOfTypeCount
	}
}
