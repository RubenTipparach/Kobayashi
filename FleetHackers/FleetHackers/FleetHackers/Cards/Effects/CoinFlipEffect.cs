using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FleetHackers.Cards.Effects
{
	[DataContract]
	public class CoinFlipEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.CoinFlip;
			}
		}

		[DataMember(Name = "winEffect")]
		public Effect WinEffect { get; set; }

		[DataMember(Name = "loseEffect")]
		public Effect LoseEffect { get; set; }

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (capitalize)
			{
				toStringBuilder.Append("Flip a coin.");
			}
			else
			{
				toStringBuilder.Append("flip a coin.");
			}

			if (WinEffect == null)
			{
				if (LoseEffect != null)
				{
					toStringBuilder.Append(" If you lose the coin flip, ");
					toStringBuilder.Append(LoseEffect.ToString(card, false));
				}
			}
			else
			{
				toStringBuilder.Append(" If you win the coin flip, ");
				toStringBuilder.Append(WinEffect.ToString(card, false));

				if (LoseEffect != null)
				{
					toStringBuilder.Append(". Otherwise, ");
					toStringBuilder.Append(LoseEffect.ToString(card, false));
				}
			}

			return toStringBuilder.ToString();
		}
	}
}
