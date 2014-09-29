using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackersLib.Cards.Enums;

namespace FleetHackersLib.Cards.AlternateCosts
{
	[DataContract]
	public class DiscardCost : AlternateCost
	{
		public override AlternateCostType AlternateCostType
		{
			get
			{
				return AlternateCostType.Discard;
			}
		}

		[DataMember(Name = "numCards")]
		public int NumCards { get; set; }

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (capitalize)
			{
				toStringBuilder.Append("Discard ");
			}
			else
			{
				toStringBuilder.Append("discard ");
			}

			if (NumCards == 1)
			{
				toStringBuilder.Append("a card");
			}
			else
			{
				toStringBuilder.Append(NumCards.ToString());
				toStringBuilder.Append("cards");
			}

			return toStringBuilder.ToString();
		}
	}
}
