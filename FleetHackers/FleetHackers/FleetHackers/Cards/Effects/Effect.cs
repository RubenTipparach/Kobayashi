using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackers.Cards.Effects.Enums;

namespace FleetHackers.Cards.Effects
{
	[DataContract]
	[KnownType(typeof(DamageEffect))]
	[KnownType(typeof(StatPumpEffect))]
	[KnownType(typeof(AnnihilateEffect))]
	[KnownType(typeof(CoinFlipEffect))]
	[KnownType(typeof(ExhaustEffect))]
	[KnownType(typeof(ForfeitEffect))]
	[KnownType(typeof(StateCheckEffect))]
	public abstract class Effect
	{
		public abstract EffectType EffectType { get; }
		public abstract string ToString(Card card, bool capitalize = false);
	}
}
