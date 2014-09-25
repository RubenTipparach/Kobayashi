using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using System.Reflection;
using FleetHackers.Cards.Effects.Enums;
using FleetHackers.Cards.Enums;

namespace FleetHackers.Cards.Effects
{
	[DataContract]
	public class StatPumpEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.StatPump;
			}
		}

		public Target Target { get; set; }

		[DataMember(Name = "target")]
		public string TargetString
		{
			get { return Target.ToString(); }
			set { Target = (Target)Enum.Parse(typeof(Target), value); }
		}

		[DataMember(Name = "attackPump")]
		public int AttackPump { get; set; }

		[DataMember(Name = "defensePump")]
		public int DefensePump { get; set; }

		public AmountType AttackPumpType { get; set; }

		[DataMember(Name = "attackPumpType")]
		public string AttackPumpTypeString
		{
			get { return AttackPumpType.ToString(); }
			set { AttackPumpType = (AmountType)Enum.Parse(typeof(AmountType), value); }
		}

		public AmountType DefensePumpType { get; set; }

		[DataMember(Name = "defensePumpType")]
		public string DefensePumpTypeString
		{
			get { return DefensePumpType.ToString(); }
			set { DefensePumpType = (AmountType)Enum.Parse(typeof(AmountType), value); }
		}

		public Variable AttackPumpVar { get; set; }

		[DataMember(Name = "attackPumpVar")]
		public string AttackPumpVarString
		{
			get { return AttackPumpVar.ToString(); }
			set { AttackPumpVar = (Variable)Enum.Parse(typeof(Variable), value); }
		}

		public Variable DefensePumpVar { get; set; }

		[DataMember(Name = "defensePumpVar")]
		public string DefensePumpVarString
		{
			get { return DefensePumpVar.ToString(); }
			set { DefensePumpVar = (Variable)Enum.Parse(typeof(Variable), value); }
		}

		[DataMember(Name = "also")]
		public bool Also { get; set; }

		public PointInTime EffectEnds { get; set; }

		[DataMember(Name = "effectEnds")]
		public string EffectEndsString
		{
			get { return EffectEnds.ToString(); }
			set { EffectEnds = (PointInTime)Enum.Parse(typeof(PointInTime), value); }
		}

		public Subtype Subtype { get; set; }

		[DataMember(Name = "subtype")]
		public string SubtypeString
		{
			get
			{
				return Subtype.ToString();
			}
			set
			{
				Subtype = (Subtype)Enum.Parse(typeof(Subtype), value);
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

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			string targetOwns = string.Empty;
			switch (Target)
			{
				case Target.This:
					toStringBuilder.Append(card.Title);
					toStringBuilder.Append(" ");
					targetOwns = "its";
					break;
				case Target.AnyShip:
					toStringBuilder.Append(capitalize ? "Target ship " : "target ship ");
					targetOwns = "that ship's";
					break;
				case Target.TargettedShip:
					toStringBuilder.Append(capitalize ? "That ship " : "that ship ");
					targetOwns = "that ship's";
					break;
				case Target.OtherShipsOfType:
					toStringBuilder.Append(capitalize ? "Other " : "other ");
					toStringBuilder.Append(Subtype.ToString());
					toStringBuilder.Append(" ships ");
					targetOwns = string.Empty;
					break;
				case Target.AttachedShip:
					toStringBuilder.Append(capitalize ? "Attached ship " : "attached ship ");
					targetOwns = "attached ship's";
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for StatPumpEffect.");
			}

			if (Also)
			{
				toStringBuilder.Append("also ");
			}

			toStringBuilder.Append("gets ");
			if (AttackPumpType == AmountType.Variable)
			{
				if (AttackPump == -1)
				{
					toStringBuilder.Append("-");
				}
				else
				{
					toStringBuilder.Append("+");
				}

				toStringBuilder.Append(Description.ToDescription(AttackPumpVar));
			}
			else
			{
				if (AttackPump > 0)
				{
					toStringBuilder.Append("+");
				}
				else if (AttackPump == 0)
				{
					if (DefensePump < 0)
					{
						toStringBuilder.Append("-");
					}
					else
					{
						toStringBuilder.Append("+");
					}
				}

				toStringBuilder.Append(AttackPump.ToString());
			}
			toStringBuilder.Append("/");
			if (DefensePumpType == AmountType.Variable)
			{
				if (DefensePump == -1)
				{
					toStringBuilder.Append("-");
				}
				else
				{
					toStringBuilder.Append("+");
				}

				toStringBuilder.Append(Description.ToDescription(DefensePumpVar));
			}
			else
			{
				if (DefensePump > 0)
				{
					toStringBuilder.Append("+");
				}
				else if (DefensePump == 0)
				{
					if (AttackPump < 0)
					{
						toStringBuilder.Append("-");
					}
					else
					{
						toStringBuilder.Append("+");
					}
				}
				toStringBuilder.Append(DefensePump.ToString());
			}

			switch (EffectEnds)
			{
				case PointInTime.None:
					break;
				case PointInTime.EndOfTurn:
					toStringBuilder.Append(" until end of turn");
					break;
				case PointInTime.BeginningOfYourTurn:
					toStringBuilder.Append(" until the beginning of your next turn");
					break;
				default:
					throw new InvalidOperationException("Unsupported EffectEnds for StatPumpEffect.");
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
							toStringBuilder.Append(string.Format(Description.ToDescription(def.ValueAttribute), targetOwns, def.Subtype.ToString()));
							break;
						default:
							throw new InvalidOperationException("Unsupported AmountType for VariableDefinition.");
					}
				}
			}

			return toStringBuilder.ToString();
		}
	}
}
