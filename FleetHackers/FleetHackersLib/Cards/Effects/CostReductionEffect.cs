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
	public class CostReductionEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.CostReduction;
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

		[DataMember(Name = "reductionAmount")]
		public int ReductionAmount { get; set; }

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (capitalize)
			{
				toStringBuilder.Append("Reduce the energy cost of ");
			}
			else
			{
				toStringBuilder.Append("reduce the energy cost of ");
			}

			switch (Target)
			{
				case Target.NextCardYouPlayThisTurn:
					toStringBuilder.Append("the next card you play this turn ");
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for CostReductionEffect.");
			}

			toStringBuilder.Append("by {");
			toStringBuilder.Append(ReductionAmount.ToString());
			toStringBuilder.Append("}");

			return toStringBuilder.ToString();
		}
	}
}
