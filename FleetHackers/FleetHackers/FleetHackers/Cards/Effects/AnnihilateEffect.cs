using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using System.Reflection;
using FleetHackers.Cards.Effects.Enums;
using FleetHackers.Cards.Enums;
using FleetHackers.Cards.Effects.Conditions;

namespace FleetHackers.Cards.Effects
{
	[DataContract]
	public class AnnihilateEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.Annihilate;
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

		[DataMember(Name = "varDefinitions")]
		private readonly List<VariableDefinition> _varDefinitions = new List<VariableDefinition>();
		private ReadOnlyCollection<VariableDefinition> _varDefinitionsView;
		public ReadOnlyCollection<VariableDefinition> VarDefinitions
		{
			get
			{
				if (_varDefinitionsView == null)
				{
					_varDefinitionsView = new ReadOnlyCollection<VariableDefinition>(_varDefinitions);
				}
				return _varDefinitionsView;
			}
		}

		[DataMember(Name = "optional")]
		public bool Optional { get; set; }

		[DataMember(Name = "condition")]
		public EffectCondition Condition { get; set; }

		[DataMember(Name = "variableBinding")]
		public VariableBinding VariableBinding { get; set; }

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (Optional)
			{
				if (capitalize)
				{
					toStringBuilder.Append("You may annihilate ");
				}
				else
				{
					toStringBuilder.Append("you may annihilate ");
				}
			}
			else
			{
				if (capitalize)
				{
					toStringBuilder.Append("Annihilate ");
				}
				else
				{
					toStringBuilder.Append("annihilate ");
				}
			}

			string targetOwns = string.Empty;

			List<string> targetStrings = new List<string>();
			foreach (Target target in _targets)
			{
				switch (target)
				{
					case Target.AnyDevice:
						targetStrings.Add("target device");
						targetOwns = "that device's";
						break;
					case Target.OtherDevice:
						targetStrings.Add("another target device");
						targetOwns = "that device's";
						break;
					case Target.AnyShip:
						targetStrings.Add("target ship");
						targetOwns = "that ship's";
						break;
					case Target.This:
						targetStrings.Add(card.Title);
						targetOwns = card.Title + "'s";
						break;
					case Target.AttackingShips:
						targetStrings.Add("all attacking ships");
						targetOwns = string.Empty;
						break;
					default:
						throw new InvalidOperationException("Unsupported Target for AnnihilateEffect.");
				}
			}
			toStringBuilder.Append(string.Join(" and ", targetStrings));

			if (Condition != null)
			{
				toStringBuilder.Append(" ");
				toStringBuilder.Append(Condition.ToString());
			}

			if (_varDefinitions != null)
			{
				bool firstDef = true;
				foreach (VariableDefinition def in _varDefinitions)
				{
					if (firstDef)
					{
						toStringBuilder.Append(", where ");

						firstDef = false;
					}
					else
					{
						toStringBuilder.Append(" and ");
					}

					toStringBuilder.Append(Description.ToDescription(def.Variable));
					toStringBuilder.Append(" is ");

					switch (def.ValueType)
					{
						case AmountType.Attribute:
							toStringBuilder.Append(string.Format(Description.ToDescription(def.ValueAttribute), targetOwns));
							break;
						default:
							throw new InvalidOperationException("Unsupported AmountType for VariableDefinition.");
					}
				}
			}

			if (VariableBinding != null)
			{
				toStringBuilder.Append(". ");
				toStringBuilder.Append(Description.ToDescription(VariableBinding.Variable));
				toStringBuilder.Append(" is ");
				string attributeDescription = Description.ToDescription(VariableBinding.Attribute);
				toStringBuilder.Append(string.Format(Description.ToDescription(VariableBinding.ValueModifier), targetOwns + " " + attributeDescription));
			}

			return toStringBuilder.ToString();
		}
	}
}
