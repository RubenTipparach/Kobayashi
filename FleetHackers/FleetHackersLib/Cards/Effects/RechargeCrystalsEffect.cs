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
	public class RechargeCrystalsEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.RechargeCrystals;
			}
		}

		[DataMember(Name = "optional")]
		public bool Optional { get; set; }

		[DataMember(Name = "exact")]
		public bool Exact { get; set; }

		[DataMember(Name = "numCrystals")]
		public int NumCrystals { get; set; }

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (Optional)
			{
				if (capitalize)
				{
					toStringBuilder.Append("You may recharge ");
				}
				else
				{
					toStringBuilder.Append("you may recharge ");
				}
			}
			else
			{
				if (capitalize)
				{
					toStringBuilder.Append("Recharge ");
				}
				else
				{
					toStringBuilder.Append("recharge ");
				}
			}

			if (NumCrystals == 1)
			{
				toStringBuilder.Append("a");
			}
			else
			{
				if (!Exact)
				{
					toStringBuilder.Append("up to ");
				}
				toStringBuilder.Append(NumCrystals.ToString());
			}

			toStringBuilder.Append(" depleted energy crystal");
			if (NumCrystals > 1)
			{
				toStringBuilder.Append("s");
			}

			return toStringBuilder.ToString();
		}
	}
}
