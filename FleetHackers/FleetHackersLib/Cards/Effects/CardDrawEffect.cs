using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackersLib.Cards.Effects.Enums;
using FleetHackersLib.Cards.Enums;

namespace FleetHackersLib.Cards.Effects
{
	[DataContract]
	public class CardDrawEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.CardDraw;
			}
		}

		[DataMember(Name = "numCards")]
		public int NumCards { get; set; }

		public Target Target { get; set; }

		[DataMember(Name = "target")]
		public string TargetString
		{
			get
			{
				return Target.ToString();
			}
			set
			{
				Target = (Target)Enum.Parse(typeof(Target), value);
			}
		}

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if ((Target == Target.You) || (Target == Target.None))
			{
				if (capitalize)
				{
					toStringBuilder.Append("Draw ");
				}
				else
				{
					toStringBuilder.Append("draw ");
				}
			}
			else if (Target == Target.TargetShipController)
			{
				if (capitalize)
				{
					toStringBuilder.Append("That ship's controller draws ");
				}
				else
				{
					toStringBuilder.Append("that ship's controller draws ");
				}
			}
			else
			{
				throw new InvalidOperationException("Unsupported Target for CardDrawEffect.");
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
