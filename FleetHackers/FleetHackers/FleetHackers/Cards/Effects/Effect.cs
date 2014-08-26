using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FleetHackers.Cards.Effects
{
	[DataContract]
	[KnownType(typeof(DamageEffect))]
	//[KnownType(typeof(StatPumpEffect))]
	public abstract class Effect
	{
		public abstract EffectType EffectType { get; }
		public abstract string ToString(Card card, bool capitalize = false);
	}
}
