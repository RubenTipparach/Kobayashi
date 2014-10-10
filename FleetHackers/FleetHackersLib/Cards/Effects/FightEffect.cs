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
	public class FightEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.Fight;
			}
		}

		public Target Actor { get; set; }

		[DataMember(Name = "actor")]
		public string ActorString
		{
			get
			{
				return Actor.ToString();
			}
			set
			{
				Actor = (Target)Enum.Parse(typeof(Target), value);
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

			switch (Actor)
			{
				case Target.This:
					toStringBuilder.Append(card.Title);
					break;
				default:
					throw new InvalidOperationException("Unsupported Actor for FightEffect.");
			}

			toStringBuilder.Append(" fights ");

			switch (ChooseMethod)
			{
				case ChooseMethod.Random:
					toStringBuilder.Append("a random ");
					break;
				default:
					throw new InvalidOperationException("Unsupported ChooseMethod for FightEffect.");
			}

			switch (Target)
			{
				case Target.OpponentShipInRange:
					toStringBuilder.Append("ship controlled by an opponent that is in range");
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for FightEffect.");
			}

			return toStringBuilder.ToString();
		}
	}
}
