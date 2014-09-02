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
	public class DiscardEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.Bounce;
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

		public ChooseMethod ChooseMethod { get; set; }

		[DataMember(Name = "chooseMethod")]
		public string ChooseMethodString
		{
			get
			{
				return ChooseMethod.ToString();
			}
			set
			{
				ChooseMethod = (ChooseMethod)Enum.Parse(typeof(ChooseMethod), value);
			}
		}

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			switch (Target)
			{
				case Target.Opponent:
					switch (ChooseMethod)
					{
						case ChooseMethod.YouChoose:
							if (capitalize)
							{
								toStringBuilder.Append("L");
							}
							else
							{
								toStringBuilder.Append("l");
							}
							toStringBuilder.Append("ook at target opponent's and and choose a card in it. That player discards the chosen card");
							break;
						default:
							throw new InvalidOperationException("Unsupported ChooseMethod for DiscardEffect.");
					}
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for DiscardEffect.");
			}

			return toStringBuilder.ToString();
		}
	}
}
