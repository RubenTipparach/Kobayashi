using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FleetHackers.Cards.AlternateCosts
{
	[DataContract]
	[KnownType(typeof(ExhaustShipCost))]
	[KnownType(typeof(RemoveCountersCost))]
	public abstract class AlternateCost
	{
		public abstract AlternateCostType AlternateCostType { get; }
		public abstract string ToString(Card card, bool capitalize = false);
	}
}
