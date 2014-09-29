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
	public class AddEnergyCrystalEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.AddEnergyCrystal;
			}
		}

		[DataMember(Name = "depleted")]
		public bool Depleted { get; set; }

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (capitalize)
			{
				toStringBuilder.Append("Add ");
			}
			else
			{
				toStringBuilder.Append("add ");
			}

			if (Depleted)
			{
				toStringBuilder.Append("a depleted ");
			}
			else
			{
				toStringBuilder.Append("an ");
			}

			toStringBuilder.Append("energy crystal to your cache");

			return toStringBuilder.ToString();
		}
	}
}
