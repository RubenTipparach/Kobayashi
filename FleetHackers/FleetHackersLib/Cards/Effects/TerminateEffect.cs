using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using System.Reflection;
using FleetHackersLib.Cards.Effects.Enums;
using FleetHackersLib.Cards.Enums;
using FleetHackersLib.Cards.Effects.Conditions;

namespace FleetHackersLib.Cards.Effects
{
	[DataContract]
	public class TerminateEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.Terminate;
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

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (capitalize)
			{
				toStringBuilder.Append("Terminate ");
			}
			else
			{
				toStringBuilder.Append("terminate ");
			}

			List<string> targetStrings = new List<string>();
			foreach (Target target in _targets)
			{
				switch (target)
				{
					case Target.ThatAbility:
						targetStrings.Add("that ability");
						break;
					default:
						throw new InvalidOperationException("Unsupported Target for TerminateEffect.");
				}
			}
			toStringBuilder.Append(string.Join(" and ", targetStrings));

			return toStringBuilder.ToString();
		}
	}
}
