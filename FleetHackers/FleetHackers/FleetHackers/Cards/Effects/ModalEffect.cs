using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackers.Cards.Effects.Enums;
using System.Collections.ObjectModel;

namespace FleetHackers.Cards.Effects
{
	[DataContract]
	public class ModalEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.Modal;
			}
		}

		[DataMember(Name = "effects")]
		private readonly List<Effect> _effects = new List<Effect>();
		private ReadOnlyCollection<Effect> _effectsView;
		public ReadOnlyCollection<Effect> Effects
		{
			get
			{
				if (_effectsView == null)
				{
					_effectsView = new ReadOnlyCollection<Effect>(_effects);
				}
				return _effectsView;
			}
		}

		public override string ToString(Card card, bool capitalize = false)
		{
			List<string> effectStrings = new List<string>();
			foreach (Effect effect in _effects)
			{
				effectStrings.Add(effect.ToString(card, capitalize && (effectStrings.Count == 0)));
			}
			return string.Join(", or ", effectStrings);
		}
	}
}
