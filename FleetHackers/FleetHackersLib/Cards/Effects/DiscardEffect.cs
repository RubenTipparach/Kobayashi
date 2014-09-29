using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackersLib.Cards.Enums;
using FleetHackersLib.Cards.Effects.Enums;

namespace FleetHackersLib.Cards.Effects
{
	[DataContract]
	public class DiscardEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.Discard;
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

		public Target CardRestriction { get; set; }

		[DataMember(Name = "cardRestriction")]
		public string CardRestrictionString
		{
			get
			{
				return CardRestriction.ToString();
			}
			set
			{
				CardRestriction = (Target)Enum.Parse(typeof(Target), value);
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

		[DataMember(Name = "variableBinding")]
		public VariableBinding VariableBinding { get; set; }

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			string cardRestriction = string.Empty;
			switch (CardRestriction)
			{
				case Target.None:
					break;
				case Target.NonInfluence:
					cardRestriction = "non-influence ";
					break;
				default:
					throw new InvalidOperationException("Unsupported CardRestriction for DiscardEffect.");
			}

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
							toStringBuilder.Append(string.Format("ook at target opponent's hand and choose a {0}card in it. That player discards the chosen card", cardRestriction));
							break;
						case ChooseMethod.TargetChooses:
							if (capitalize)
							{
								toStringBuilder.Append("Target ");
							}
							else
							{
								toStringBuilder.Append("target ");
							}
							toStringBuilder.Append("opponent discards a card");
							break;
						default:
							throw new InvalidOperationException("Unsupported ChooseMethod for DiscardEffect.");
					}
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for DiscardEffect.");
			}

			if (VariableBinding != null)
			{
				toStringBuilder.Append(". ");
				toStringBuilder.Append(Description.ToDescription(VariableBinding.Variable));
				toStringBuilder.Append(" is ");
				string attributeDescription = Description.ToDescription(VariableBinding.Attribute);
				toStringBuilder.Append(string.Format(Description.ToDescription(VariableBinding.ValueModifier), "the " + attributeDescription + " of the discarded card"));
			}

			return toStringBuilder.ToString();
		}
	}
}
