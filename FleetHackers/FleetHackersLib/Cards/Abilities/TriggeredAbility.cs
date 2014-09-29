using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FleetHackersLib.Cards.Effects;
using System.Runtime.Serialization;
using FleetHackersLib.Cards.Enums;
using System.Collections.ObjectModel;

namespace FleetHackersLib.Cards.Abilities
{
	[DataContract]
	public class TriggeredAbility : Ability
	{
		public override AbilityType AbilityType
		{
			get
			{
				return AbilityType.Triggered;
			}
		}

		[DataMember(Name="trigger")]
		public Trigger Trigger { get; set; }

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
			if ((card.Subtype & Subtype.Trap) != 0)
			{
				StringBuilder toStringBuilder = new StringBuilder("The next time ");
				toStringBuilder.Append(Trigger.ToString(card));
				if (Trigger.VariableBinding == null)
				{
					toStringBuilder.Append(", ");
				}
				else
				{
					toStringBuilder.Append(". ");
				}
				List<string> effectStrings = new List<string>();
				bool ucase = false;
				foreach (Effect effect in Effects)
				{
					effectStrings.Add(effect.ToString(card, ucase) + "." + (effect.HasReminderText ? " " + effect.ReminderText : string.Empty));
					ucase = true;
				}
				toStringBuilder.Append(string.Join(" ", effectStrings));

				return toStringBuilder.ToString();
			}
			else if (card.Supertype != Supertype.Maneuver)
			{
				StringBuilder toStringBuilder = new StringBuilder();
				if (Trigger.TriggerType == TriggerType.EntersTheBattleZone || Trigger.TriggerType == TriggerType.LeavesTheBattleZone)
				{
					toStringBuilder.Append("When ");
				}
				else if (Trigger.TriggerType == TriggerType.Attack || Trigger.TriggerType == TriggerType.Interception || Trigger.TriggerType == TriggerType.LifeLoss || Trigger.TriggerType == TriggerType.AssaultDamage || Trigger.TriggerType == TriggerType.Damage)
				{
					toStringBuilder.Append("Whenever ");
				}
				else if (Trigger.TriggerType == TriggerType.Annihilated)
				{
					if (Trigger.Condition == Condition.None)
					{
						toStringBuilder.Append("When ");
					}
					else
					{
						toStringBuilder.Append("If ");
					}
				}

				toStringBuilder.Append(Trigger.ToString(card));
				if (Trigger.VariableBinding == null)
				{
					toStringBuilder.Append(", ");
				}
				else
				{
					toStringBuilder.Append(". ");
				}
				List<string> effectStrings = new List<string>();
				bool ucase = false;
				foreach (Effect effect in Effects)
				{
					effectStrings.Add(effect.ToString(card, ucase) + "." + (effect.HasReminderText ? " " + effect.ReminderText : string.Empty));
					ucase = true;
				}
				if (Trigger.Replacement)
				{
					toStringBuilder.Append("instead ");
				}
				toStringBuilder.Append(string.Join(" ", effectStrings));

				return toStringBuilder.ToString();
			}
			else
			{
				throw new InvalidOperationException("Unsupported Card Type for TriggeredAbility.");
			}
		}
	}
}
