using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetHackers.Cards.Enums
{
	public enum CardAttribute
	{
		None,

		[Description("{0} attack")]
		Attack,

		[Description("the number of influence cards controlled by {0} controller")]
		ControllersInfluence
	}
}
