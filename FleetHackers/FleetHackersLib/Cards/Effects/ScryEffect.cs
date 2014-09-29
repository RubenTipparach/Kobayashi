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
	public class ScryEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.Scry;
			}
		}

		[DataMember(Name = "numCards")]
		public int NumCards { get; set; }

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (capitalize)
			{
				toStringBuilder.Append("Look ");
			}
			else
			{
				toStringBuilder.Append("look ");
			}

			if (NumCards == 1)
			{
				toStringBuilder.Append("at the top card of your deck. You may put that card on the bottom of your deck");
			}
			else
			{
				toStringBuilder.Append("at the top ");
				toStringBuilder.Append(NumCards.ToString());
				toStringBuilder.Append(" cards of your deck. You may put any number of those cards on the bottom of your deck in any order. Put the remaining cards on the top of your deck in any order");
			}

			return toStringBuilder.ToString();
		}
	}
}
