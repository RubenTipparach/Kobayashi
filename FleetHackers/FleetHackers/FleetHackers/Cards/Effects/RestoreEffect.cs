using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FleetHackers.Cards.Effects.Enums;
using System.Runtime.Serialization;
using FleetHackers.Cards.Enums;
using System.Collections.ObjectModel;
using System.Reflection;

namespace FleetHackers.Cards.Effects
{
	[DataContract]
	public class RestoreEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.Restore;
			}
		}

		public Target JunkyardOwner { get; set; }

		[DataMember(Name = "junkyardOwner")]
		public string JunkyardOwnerString
		{
			get
			{
				return JunkyardOwner.ToString();
			}
			set
			{
				JunkyardOwner = (Target)Enum.Parse(typeof(Target), value);
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

		public Zone TargetZone { get; set; }

		[DataMember(Name = "targetZone")]
		public string TargetZoneString
		{
			get
			{
				return TargetZone.ToString();
			}
			set
			{
				TargetZone = (Zone)Enum.Parse(typeof(Zone), value);
			}
		}

		public Position Position { get; set; }

		[DataMember(Name = "position")]
		public string PositionString
		{
			get
			{
				return Position.ToString();
			}
			set
			{
				Position = (Position)Enum.Parse(typeof(Position), value);
			}
		}

		private readonly List<CheckStateType> _states = new List<CheckStateType>();
		private ReadOnlyCollection<CheckStateType> _statesView;
		public ReadOnlyCollection<CheckStateType> States
		{
			get
			{
				if (_statesView == null)
				{
					_statesView = new ReadOnlyCollection<CheckStateType>(_states);
				}
				return _statesView;
			}
		}

		[DataMember(Name = "states")]
		private List<String> StatesStrings
		{
			get
			{
				List<string> stringList = new List<string>();
				foreach (CheckStateType cst in _states)
				{
					stringList.Add(cst.ToString());
				}
				return stringList;
			}
			set
			{
				_states.Clear();
				foreach (string str in value)
				{
					_states.Add((CheckStateType)Enum.Parse(typeof(CheckStateType), str));
				}
			}
		}

		[OnDeserializing]
		private void OnDeserializing(StreamingContext c)
		{
			if (_states == null)
			{
				var field = GetType().GetField("_states", BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.NonPublic);
				field.SetValue(this, new List<CheckStateType>());
			}
		}

		[DataMember(Name = "optional")]
		public bool Optional { get; set; }

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (Optional)
			{
				if (capitalize)
				{
					toStringBuilder.Append("You may return ");
				}
				else
				{
					toStringBuilder.Append("you may return ");
				}
			}
			else
			{
				if (capitalize)
				{
					toStringBuilder.Append("Return ");
				}
				else
				{
					toStringBuilder.Append("return ");
				}
			}

			switch (Target)
			{
				case Target.AnyShip:
					toStringBuilder.Append("target ship");
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for RestoreEffect.");
			}

			toStringBuilder.Append(" from ");
			
			switch(JunkyardOwner) {
				case Target.You:
					toStringBuilder.Append("your");
					break;
				case Target.Opponent:
					toStringBuilder.Append("an opponent's");
					break;
				default:
					throw new InvalidOperationException("Unsupported JunkyardOwner for RestoreEffect.");
			}
			
			toStringBuilder.Append(" junkyard to ");

			switch (TargetZone)
			{
				case Zone.YourHand:
					toStringBuilder.Append("your hand");
					break;
				case Zone.BattleZone:
					toStringBuilder.Append("the battle zone");
					break;
				default:
					throw new InvalidOperationException("Unsupported TargetZone for RestoreEffect.");
			}

			switch (Position)
			{
				case Position.None:
					break;
				case Position.AdjacentToAttacking:
					toStringBuilder.Append(" adjacent to an attacking ship");
					break;
				default:
					throw new InvalidOperationException("Unsupported Position for RestoreEffect.");
			}

			List<string> stateStrings = new List<string>();
			if (_states != null)
			{
				foreach (CheckStateType state in _states)
				{
					switch (state)
					{
						case CheckStateType.AttackingInTheSameDirection:
							stateStrings.Add("attacking in the same direction");
							break;
						case CheckStateType.Exhausted:
							stateStrings.Add("exhausted");
							break;
						default:
							throw new InvalidOperationException("Unsupported State for RestoreEffect.");
					}
				}
			}
			if (stateStrings.Count > 0)
			{
				toStringBuilder.Append(", ");
				toStringBuilder.Append(string.Join(" and ", stateStrings));
			}

			return toStringBuilder.ToString();
		}
	}
}
