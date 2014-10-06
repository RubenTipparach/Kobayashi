using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using System.Reflection;
using FleetHackersLib.Cards.Effects.Enums;
using FleetHackersLib.Cards.Enums;

namespace FleetHackersLib.Cards.Effects
{
	[DataContract]
	public class NoRulesTextEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.NoRulesText;
			}
		}

		public Target Target { get; set; }

		[DataMember(Name = "target")]
		public string TargetString
		{
			get { return Target.ToString(); }
			set { Target = (Target)Enum.Parse(typeof(Target), value); }
		}

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			switch (Target)
			{
				case Target.AttachedShip:
					if (capitalize)
					{
						toStringBuilder.Append("Attached ship");
					}
					else
					{
						toStringBuilder.Append("attached ship");
					}
					break;
				case Target.This:
					toStringBuilder.Append(card.Title);
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for NoRulesTextEffect.");
			}

			toStringBuilder.Append(" loses all rules text except its base range");

			return toStringBuilder.ToString();
		}
	}
}
