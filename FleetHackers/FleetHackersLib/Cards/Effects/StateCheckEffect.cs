using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using FleetHackersLib.Cards.Effects.Enums;
using FleetHackersLib.Cards.Enums;

namespace FleetHackersLib.Cards.Effects
{
	[DataContract]
	public class StateCheckEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.StateCheck;
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

		[DataMember(Name = "checks")]
		private List<StateCheck> _checks = new List<StateCheck>();
		private ReadOnlyCollection<StateCheck> _checksView;
		public ReadOnlyCollection<StateCheck> Checks
		{
			get
			{
				if (_checksView == null)
				{
					_checksView = new ReadOnlyCollection<StateCheck>(_checks);
				}
				return _checksView;
			}
		}

		[DataMember(Name = "resultEffect")]
		public Effect ResultEffect { get; set; }

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (capitalize)
			{
				toStringBuilder.Append("If ");
			}
			else
			{
				toStringBuilder.Append("if ");
			}

			switch (Target)
			{
				case Target.This:
					toStringBuilder.Append(card.Title);
					toStringBuilder.Append(" ");
					break;
				case Target.None:
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for StateCheckEffect.");
			}

			List<string> checkStrings = new List<string>();
			foreach (StateCheck check in Checks)
			{
				switch (check.State)
				{
					case CheckStateType.CouldHaveAttackedThisTurn:
						if (check.Test)
						{
							checkStrings.Add("could have attacked this turn");
						}
						else
						{
							checkStrings.Add("couldn't have attacked this turn");
						}
						break;
					case CheckStateType.SummoningSick:
						if (check.Test)
						{
							checkStrings.Add("didn't start the turn under your control");
						}
						else
						{
							checkStrings.Add("started the turn under your control");
						}
						break;
					case CheckStateType.AttackedThisTurn:
						if (check.Test)
						{
							checkStrings.Add("attacked this turn");
						}
						else
						{
							checkStrings.Add("didn't attack this turn");
						}
						break;
					case CheckStateType.YourTurn:
						if (check.Test)
						{
							checkStrings.Add("it's your turn");
						}
						else
						{
							checkStrings.Add("it's not your turn");
						}
						break;
					default:
						throw new InvalidOperationException("Unsupported StateCheck for StateCheckEffect.");
				}
			}

			toStringBuilder.Append(string.Join(" and ", checkStrings));

			toStringBuilder.Append(", ");
			toStringBuilder.Append(ResultEffect.ToString(card, false));

			return toStringBuilder.ToString();
		}
	}
}
