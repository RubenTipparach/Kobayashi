using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackersLib.Cards.Enums;

namespace FleetHackersLib.Cards.AlternateCosts
{
	[DataContract]
	public class RemoveCountersCost : AlternateCost
	{
		public override AlternateCostType AlternateCostType
		{
			get
			{
				return AlternateCostType.RemoveCounters;
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

		[DataMember(Name = "numCounters")]
		public int NumCounters { get; set; }

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (capitalize)
			{
				toStringBuilder.Append("Remove ");
			}
			else
			{
				toStringBuilder.Append("remove ");
			}

			toStringBuilder.Append(NumCounters.ToString());
			toStringBuilder.Append(" counters from ");

			switch (Target)
			{
				case Target.This:
					toStringBuilder.Append(card.Title);
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for RemoveCountersCost.");
			}

			return toStringBuilder.ToString();
		}
	}
}
