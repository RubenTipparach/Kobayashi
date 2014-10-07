using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackersLib.Cards.Enums;
using FleetHackersLib.Cards.Effects.Enums;
using FleetHackersLib.Cards.Effects.Conditions;

namespace FleetHackersLib.Cards.Effects
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

		public Target AttachTarget { get; set; }

		[DataMember(Name = "attachTarget")]
		public string AttachTargetString
		{
			get
			{
				return AttachTarget.ToString();
			}
			set
			{
				AttachTarget = (Target)Enum.Parse(typeof(Target), value);
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

		public Subtype CardSubtype { get; set; }

		[DataMember(Name = "cardSubtype")]
		public string CardSubtypeString
		{
			get
			{
				return CardSubtype.ToString();
			}
			set
			{
				CardSubtype = (Subtype)Enum.Parse(typeof(Subtype), value);
			}
		}

		[DataMember(Name = "cardTitle")]
		public string CardTitle { get; set; }

		[DataMember(Name = "optional")]
		public bool Optional { get; set; }

		[DataMember(Name = "playImmediate")]
		public bool PlayImmediate { get; set; }

		[DataMember(Name = "condition")]
		public EffectCondition Condition { get; set; }

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

			if ((!Optional) && capitalize)
			{
				toStringBuilder.Append("Put target ");
			}
			else
			{
				toStringBuilder.Append("put target ");
			}

			if (CardSubtype != Subtype.None)
			{
				toStringBuilder.Append(CardSubtype.ToString());
				toStringBuilder.Append(" ");
			}
			if (CardType != Supertype.None)
			{
				toStringBuilder.Append(CardType.ToString().ToLower());
				toStringBuilder.Append(" ");
			}

			toStringBuilder.Append("card");

			if (!string.IsNullOrWhiteSpace(CardTitle))
			{
				toStringBuilder.Append(" named ");
				toStringBuilder.Append(CardTitle);
			}
			if (Condition != null)
			{
				toStringBuilder.Append(" ");
				toStringBuilder.Append(Condition.ToString());
			}

			toStringBuilder.Append(" from your deck into ");

			if (PlayImmediate)
			{
				toStringBuilder.Append("the battle zone");

				switch (AttachTarget)
				{
					case Target.None:
						break;
					case Target.This:
						toStringBuilder.Append(", attached to ");
						toStringBuilder.Append(card.Title);
						break;
					default:
						throw new InvalidOperationException("Unsupported AttachTarget for TutorEffect.");
				}
			}
			else
			{
				toStringBuilder.Append("your hand");
			}

			return toStringBuilder.ToString();
		}
	}
}
