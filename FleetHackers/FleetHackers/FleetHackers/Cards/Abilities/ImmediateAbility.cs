using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackers.Cards.Effects;
using System.Collections.ObjectModel;
using FleetHackers.Cards.Enums;

namespace FleetHackers.Cards.Abilities
{
	[DataContract]
	public class ImmediateAbility : Ability
	{
		public override AbilityType AbilityType
		{
			get
			{
				return AbilityType.Immediate;
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

		public override string ToString(Card card)
		{
			if (card.Supertype == Supertype.Maneuver)
			{
				StringBuilder toStringBuilder = new StringBuilder();

				List<string> effectStrings = new List<string>();
				foreach (Effect effect in Effects)
				{
					effectStrings.Add(effect.ToString(card, true) + ".");
				}
				toStringBuilder.Append(string.Join(Environment.NewLine, effectStrings));

				return toStringBuilder.ToString();
			}
			else
			{
				throw new InvalidOperationException("Unsupported Card Type for ImmediateAbility.");
			}
		}
	}
}
