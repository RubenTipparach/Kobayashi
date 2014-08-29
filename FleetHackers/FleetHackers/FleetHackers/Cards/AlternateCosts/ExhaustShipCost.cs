﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackers.Cards.Enums;

namespace FleetHackers.Cards.AlternateCosts
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

		public override string ToString(Card card)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			toStringBuilder.Append("exhaust ");

			if (NumTargetsType == AmountType.Numeric)
			{
				toStringBuilder.Append(NumTargets.ToString());
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

			switch (Target)
			{
				case Target.YourChargedShips:
					toStringBuilder.Append(" charged ships you control.");
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for ExhaustShipCost.");
			}

			if (NumTargetsType == AmountType.Variable)
			{
				if (!((card.EnergyCostType == AmountType.Variable) && (Description.ToDescription(card.EnergyCostVar).Contains(NumTargetsVar.ToString()))))
				{
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

					toStringBuilder.Append(" you exhausted this way.");
				}
			}

			return toStringBuilder.ToString();
		}
	}
}
