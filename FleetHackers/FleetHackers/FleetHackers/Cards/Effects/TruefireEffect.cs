using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackers.Cards.Effects.Enums;
using System.Collections.ObjectModel;
using System.Reflection;

namespace FleetHackers.Cards.Effects
{
	[DataContract]
	public class TruefireEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.Truefire;
			}
		}

		public Target Target { get; set; }

		[DataMember(Name = "target")]
		public string TargetString
		{
			get { return Target.ToString(); }
			set { Target = (Target)Enum.Parse(typeof(Target), value); }
		}

		[DataMember(Name = "optional")]
		public bool Optional { get; set; }

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (Optional)
			{
				if (capitalize)
				{
					toStringBuilder.Append("You may choose to assign ");
				}
				else
				{
					toStringBuilder.Append("you may choose to assign ");
				}
			}
			else
			{
				if (capitalize)
				{
					toStringBuilder.Append("Assign ");
				}
				else
				{
					toStringBuilder.Append("assign ");
				}
			}

			switch (Target)
			{
				case Target.This:
					toStringBuilder.Append(card.Title);
					toStringBuilder.Append("'s ");
					break;
				default:
					throw new InvalidOperationException("Unsupported Actor for DamageEffect.");
			}

			toStringBuilder.Append("battle damage as though it wasn't intecepted");

			return toStringBuilder.ToString();
		}
	}
}
