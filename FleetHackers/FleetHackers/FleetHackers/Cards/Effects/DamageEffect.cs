using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Reflection;

namespace FleetHackers.Cards.Effects
{
	[DataContract]
	public class DamageEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.Damage;
			}
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

		[DataMember(Name="actor")]
		public string ActorString
		{
			get { return Actor.ToString(); }
			set { Actor = (Target)Enum.Parse(typeof(Target), value); }
		}

		[DataMember(Name = "amount")]
		public int Amount { get; set; }

		[DataMember(Name = "exact")]
		public bool Exact { get; set; }

		[DataMember(Name = "optional")]
		public bool Optional { get; set; }

		public DivideMethod DivideMethod { get; set; }

		[DataMember(Name = "divideMethod")]
		public string DivideMethodString
		{
			get { return DivideMethod.ToString(); }
			set { DivideMethod = (DivideMethod)Enum.Parse(typeof(DivideMethod), value); }
		}

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (Optional)
			{
				toStringBuilder.Append("you may have ");
			}

			switch (Actor)
			{
				case Target.This:
					toStringBuilder.Append(card.Title);
					toStringBuilder.Append(" ");
					break;
				default:
					throw new InvalidOperationException("Unsupported Actor for DamageEffect.");
			}

			if (Optional)
			{
				toStringBuilder.Append("inflict ");
			}
			else
			{
				toStringBuilder.Append("inflicts ");
			}

			if (!Exact)
			{
				toStringBuilder.Append("up to ");
			}

			if (Amount < 0)
			{
				toStringBuilder.Append("X");
				for (int i = 1; i < Math.Abs(Amount); i++)
				{
					toStringBuilder.Append("+X");
				}
			}
			else
			{
				toStringBuilder.Append(Amount.ToString());
			}
			toStringBuilder.Append(" damage ");

			switch (DivideMethod)
			{
				case DivideMethod.YouChooseAny:
					toStringBuilder.Append("divided however you choose among ");
					break;
				case DivideMethod.None:
					toStringBuilder.Append("to ");
					break;
				default:
					throw new InvalidOperationException("Unsupported DivideMethod for DamageEffect.");
			}

			List<string> targetStrings = new List<string>();
			foreach (Target target in _targets)
			{
				switch (target)
				{
					case Target.AttackingShips:
						targetStrings.Add("the attacking ships");
						break;
					case Target.AnyShip:
						targetStrings.Add("target ship");
						break;
					case Target.UpTo2OtherShips:
						targetStrings.Add("up to 2 other target ships");
						break;
					default:
						throw new InvalidOperationException("Unsupported Target for DamageEffect.");
				}
			}
			toStringBuilder.Append(string.Join(" or ", targetStrings));

			return toStringBuilder.ToString();
		}
	}
}
