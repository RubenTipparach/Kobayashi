using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackers.Cards.Enums;
using FleetHackers.Cards.Effects.Enums;

namespace FleetHackers.Cards.Effects
{
	[DataContract]
	public class TutorEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.Tutor;
			}
		}

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

		public Supertype CardType { get; set; }

		[DataMember(Name = "cardType")]
		public string CardTypeString
		{
			get
			{
				return CardType.ToString();
			}
			set
			{
				CardType = (Supertype)Enum.Parse(typeof(Supertype), value);
			}
		}

		[DataMember(Name = "optional")]
		public bool Optional { get; set; }

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			switch (Target)
			{
				case Target.You:
					if (Optional)
					{
						if (capitalize)
						{
							toStringBuilder.Append("You ");
						}
						else
						{
							toStringBuilder.Append("you ");
						}
					}
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for TutorEffect.");
			}

			if (Optional)
			{
				toStringBuilder.Append("may ");
			}

			toStringBuilder.Append("put target ");
			toStringBuilder.Append(CardType.ToString().ToLower());
			toStringBuilder.Append(" from your deck into your hand");

			return toStringBuilder.ToString();
		}
	}
}
