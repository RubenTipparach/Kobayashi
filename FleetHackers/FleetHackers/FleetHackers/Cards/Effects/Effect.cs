using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetHackers.Cards.Effects
{
	public abstract class Effect
	{
		public abstract EffectType EffectType { get; }
		public abstract string ToString(Card card);
	}
}
