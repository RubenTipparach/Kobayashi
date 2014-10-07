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
	public class ProvidesInfluenceEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.ProvidesInfluence;
			}
		}

		[DataMember(Name = "influenceAmount")]
		public int InfluenceAmount { get; set; }

		public override string ToString(Card card, bool capitalize = false)
		{
			string symbol = string.Empty;
			switch (card.Alignment)
			{
				case Alignment.Crystal:
					symbol = "{W}";
					break;
				case Alignment.Cryo:
					symbol = "{U}";
					break;
				case Alignment.Shadow:
					symbol = "{B}";
					break;
				case Alignment.Pyre:
					symbol = "{R}";
					break;
				case Alignment.Xeno:
					symbol = "{G}";
					break;
				default:
					throw new InvalidOperationException("Unsupported Alignment for ProvidesInfluenceEffect.");
			}

			return card.Title + " provides " + String.Concat(Enumerable.Repeat(symbol, InfluenceAmount));
		}
	}
}
