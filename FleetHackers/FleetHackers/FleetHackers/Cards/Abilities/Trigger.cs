using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Reflection;
using FleetHackers.Cards.Enums;

namespace FleetHackers.Cards.Abilities
{
	[DataContract]
	public class Trigger
	{

		public TriggerType TriggerType { get; set; }

		[DataMember(Name = "triggerType")]
		public string TriggerTypeString
		{
			get { return TriggerType.ToString(); }
			set { TriggerType = (TriggerType)Enum.Parse(typeof(TriggerType), value); }
		}

		private readonly List<Target> _targets = new List<Target>();
		private ReadOnlyCollection<Target> _targetsView;
		public ReadOnlyCollection<Target> Targets
		{
			get
			{
				if (_targetsView == null)
				{
					_targetsView = new ReadOnlyCollection<Target>(_targets);
				}
				return _targetsView;
			}
		}

		[DataMember(Name = "targets")]
		private List<String> TargetsStrings
		{
			get
			{
				List<string> stringList = new List<string>();
				foreach (Target tgt in _targets)
				{
					stringList.Add(tgt.ToString());
				}
				return stringList;
			}
			set
			{
				_targets.Clear();
				foreach (string str in value)
				{
					_targets.Add((Target)Enum.Parse(typeof(Target), str));
				}
			}
		}

		[OnDeserializing]
		private void OnDeserializing(StreamingContext c)
		{
			if (_targets == null)
			{
				var field = GetType().GetField("_targets", BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.NonPublic);
				field.SetValue(this, new List<Target>());
			}
		}

		public Target Actor { get; set; }

		[DataMember(Name = "actor")]
		public string ActorString
		{
			get { return Actor.ToString(); }
			set { Actor = (Target)Enum.Parse(typeof(Target), value); }
		}

		public Condition Condition { get; set; }

		[DataMember(Name = "condition")]
		public string ConditionString
		{
			get { return Condition.ToString(); }
			set { Condition = (Condition)Enum.Parse(typeof(Condition), value); }
		}

		public string ToString(Card card)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			switch (Actor)
			{
				case Target.OpponentShip:
					toStringBuilder.Append("a ship controlled by an opponent ");
					break;
				case Target.AnyShip:
					toStringBuilder.Append("any ship ");
					break;
				case Target.This:
					toStringBuilder.Append(card.Title);
					toStringBuilder.Append(" ");
					break;
				case Target.None:
					break;
				case Target.You:
					toStringBuilder.Append("your home base ");
					break;
				default:
					throw new InvalidOperationException("Unsupported Actor for Trigger.");
			}

			switch (TriggerType)
			{
				case TriggerType.Attack:
					toStringBuilder.Append("attacks");
					break;
				case TriggerType.Interception:
					toStringBuilder.Append("intercepts");
					break;
				case TriggerType.EntersTheBattleZone:
					toStringBuilder.Append("enters the battle zone");
					break;
				case TriggerType.EndOfYourTurn:
					toStringBuilder.Append("At the end of your turn");
					break;
				case TriggerType.LeavesTheBattleZone:
					toStringBuilder.Append("leaves the battle zone");
					break;
				case TriggerType.Annihilated:
					toStringBuilder.Append("is annihilated");
					break;
				case TriggerType.LifeLoss:
					toStringBuilder.Append("loses health");
					break;
				default:
					throw new InvalidOperationException("Unsupported TriggerType for Trigger.");
			}

			List<string> targetStrings = new List<string>();
			foreach (Target target in _targets)
			{
				switch (target)
				{
					case Target.YourShip:
						targetStrings.Add("a ship you control");
						break;
					case Target.YourHomeBase:
						targetStrings.Add("your home base");
						break;
					case Target.This:
						targetStrings.Add(card.Title);
						break;
					default:
						throw new InvalidOperationException("Unsupported Target for Trigger.");
				}
			}
			if (targetStrings.Count > 0)
			{
				toStringBuilder.Append(" ");
			}
			toStringBuilder.Append(string.Join(" or ", targetStrings));

			switch (Condition)
			{
				case Condition.None:
					break;
				case Condition.OneOtherShip:
					toStringBuilder.Append(" with at least 1 other ship");
					break;
				case Condition.DuringCombat:
					toStringBuilder.Append(" during a battle");
					break;
				default:
						throw new InvalidOperationException("Unsupported Condition for Trigger.");
			}

			return toStringBuilder.ToString();
		}
	}
}
