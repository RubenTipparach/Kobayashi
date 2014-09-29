using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackersLib.Cards.Enums;

namespace FleetHackersLib.Cards.AlternateCosts
{
	[DataContract]
	public class ExhaustShipCost : AlternateCost
	{
		public override AlternateCostType AlternateCostType
		{
			get
			{
				return AlternateCostType.ExhaustShip;
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

		[DataMember(Name = "numTargets")]
		public int NumTargets { get; set; }

		public AmountType NumTargetsType { get; set; }

		[DataMember(Name = "numTargetsType")]
		public string NumTargetsTypeString
		{
			get
			{
				return NumTargetsType.ToString();
			}
			set
			{
				NumTargetsType = (AmountType)Enum.Parse(typeof(AmountType), value);
			}
		}

		public Variable NumTargetsVar { get; set; }

		[DataMember(Name = "numTargetsVar")]
		public string NumTargetsVarString
		{
			get
			{
				return NumTargetsVar.ToString();
			}
			set
			{
				NumTargetsVar = (Variable)Enum.Parse(typeof(Variable), value);
			}
		}

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (capitalize)
			{
				toStringBuilder.Append("Exhaust ");
			}
			else
			{
				toStringBuilder.Append("exhaust ");
			}

			if (Target != Target.This)
			{
				if (NumTargetsType == AmountType.Numeric)
				{
					if (NumTargets == 1)
					{
						toStringBuilder.Append("target");
					}
					else
					{
						toStringBuilder.Append(NumTargets.ToString());
					}
				}
				else
				{
					if ((card.EnergyCostType == AmountType.Variable) && (Description.ToDescription(card.EnergyCostVar).Contains(NumTargetsVar.ToString())))
					{
						toStringBuilder.Append("any number of");
					}
					else
					{
						toStringBuilder.Append(Description.ToDescription(NumTargetsVar));
					}
				}
			}

			switch (Target)
			{
				case Target.YourChargedShips:
					toStringBuilder.Append(string.Format(" charged ship{0} you control", NumTargets == 1 ? string.Empty : "s"));
					break;
				case Target.This:
					toStringBuilder.Append(card.Title);
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for ExhaustShipCost.");
			}

			if (NumTargetsType == AmountType.Variable)
			{
				if (!((card.EnergyCostType == AmountType.Variable) && (Description.ToDescription(card.EnergyCostVar).Contains(NumTargetsVar.ToString()))))
				{
					toStringBuilder.Append(". ");
					toStringBuilder.Append(Description.ToDescription(NumTargetsVar));
					toStringBuilder.Append(" is the number of ");

					switch (Target)
					{
						case Target.YourChargedShips:
							toStringBuilder.Append("ships");
							break;
						default:
							throw new InvalidOperationException("Unsupported Target for ExhaustShipCost.");
					}

					toStringBuilder.Append(" you exhausted this way");
				}
			}

			return toStringBuilder.ToString();
		}
	}
}
