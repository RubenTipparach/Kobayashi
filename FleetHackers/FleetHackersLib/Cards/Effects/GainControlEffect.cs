using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using System.Reflection;
using FleetHackersLib.Cards.Effects.Enums;
using FleetHackersLib.Cards.Enums;

namespace FleetHackersLib.Cards.Effects
{
	[DataContract]
	public class GainControlEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.GainControl;
			}
		}

		public Target Target { get; set; }

		[DataMember(Name = "target")]
		public string TargetString
		{
			get { return Target.ToString(); }
			set { Target = (Target)Enum.Parse(typeof(Target), value); }
		}

		public Target Controller { get; set; }

		[DataMember(Name = "controller")]
		public string ControllerString
		{
			get { return Controller.ToString(); }
			set { Controller = (Target)Enum.Parse(typeof(Target), value); }
		}

		public PointInTime EffectEnds { get; set; }

		[DataMember(Name = "effectEnds")]
		public string EffectEndsString
		{
			get { return EffectEnds.ToString(); }
			set { EffectEnds = (PointInTime)Enum.Parse(typeof(PointInTime), value); }
		}

		[DataMember(Name = "successEffects")]
		private readonly List<Effect> _successEffects = new List<Effect>();
		private ReadOnlyCollection<Effect> _successEffectsView;
		public ReadOnlyCollection<Effect> SuccessEffects
		{
			get
			{
				if (_successEffectsView == null)
				{
					_successEffectsView = new ReadOnlyCollection<Effect>(_successEffects);
				}
				return _successEffectsView;
			}
		}

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (capitalize)
			{
				toStringBuilder.Append("You gain control of ");
			}
			else
			{
				toStringBuilder.Append("you gain control of ");
			}

			switch (Target)
			{
				case Target.ChosenDevice:
					toStringBuilder.Append("the chosen device");
					break;
				case Target.RandomDevice:
					toStringBuilder.Append("a random device");
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for GainControlEffect.");
			}

			if (Controller != Target.None)
			{
				toStringBuilder.Append(" controlled by");
			}

			switch (Controller)
			{
				case Target.None:
					break;
				case Target.HomeBaseController:
					toStringBuilder.Append(" that home base's controller");
					break;
				default:
					throw new InvalidOperationException("Unsupported Controller for GainControlEffect.");
			}

			switch (EffectEnds)
			{
				case PointInTime.EndOfTurn:
					toStringBuilder.Append(" until end of turn");
					break;
				case PointInTime.BeginningOfYourTurn:
					toStringBuilder.Append(" until the beginning of your next turn");
					break;
				case PointInTime.None:
					break;
				default:
					throw new InvalidOperationException("Unsupported EffectEnds for GainControlEffect.");
			}

			if ((_successEffects != null) && (_successEffects.Count > 0))
			{
				toStringBuilder.Append(". If you do, ");

				List<string> effectStrings = new List<string>();
				bool firstEffect = true;
				foreach (Effect effect in _successEffects)
				{
					if (firstEffect)
					{
						effectStrings.Add(effect.ToString(card, false));

						firstEffect = false;
					}
					else
					{
						effectStrings.Add(effect.ToString(card, true));
					}
				}
				toStringBuilder.Append(string.Join(". ", effectStrings));
			}

			return toStringBuilder.ToString();
		}
	}
}
