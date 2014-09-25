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
	public class MillEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.Mill;
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

			string pronoun = string.Empty;
			if (Target == Target.Opponent)
			{
				pronoun = "his or her";
				if (capitalize)
				{
					toStringBuilder.Append("Target opponent puts ");
				}
				else
				{
					toStringBuilder.Append("target opponent puts ");
				}
			}
			else
			{
				throw new InvalidOperationException("Unsupported Target for MillEffect.");
			}

			if (NumCards == 1)
			{
				toStringBuilder.Append("the top card ");
			}
			else
			{
				toStringBuilder.Append("the top ");
				toStringBuilder.Append(NumCards.ToString());
				toStringBuilder.Append(" cards ");
			}

			toStringBuilder.Append("of ");
			toStringBuilder.Append(pronoun);
			toStringBuilder.Append(" deck into ");
			toStringBuilder.Append(pronoun);
			toStringBuilder.Append(" junkyard");

			return toStringBuilder.ToString();
		}
	}
}
