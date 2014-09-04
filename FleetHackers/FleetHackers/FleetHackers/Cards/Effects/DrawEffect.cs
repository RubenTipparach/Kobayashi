using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackers.Cards.Effects.Enums;
using FleetHackers.Cards.Enums;

namespace FleetHackers.Cards.Effects
{
	[DataContract]
	public class DrawEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.Draw;
			}
		}

		[DataMember(Name = "numCards")]
		public int NumCards { get; set; }

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (capitalize)
			{
				toStringBuilder.Append("Draw ");
			}
			else
			{
				toStringBuilder.Append("draw ");
			}

			if (NumCards == 1)
			{
				toStringBuilder.Append("a card");
			}
			else
			{
				toStringBuilder.Append(NumCards.ToString());
				toStringBuilder.Append(" cards");
			}

			return toStringBuilder.ToString();
		}
	}
}
