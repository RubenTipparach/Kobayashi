using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackers.Cards.Effects.Enums;
using System.Collections.ObjectModel;
using FleetHackers.Cards.Enums;

namespace FleetHackers.Cards.Effects
{
	[DataContract]
	public class ChooseTargetEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.ChooseTarget;
			}
		}

		public Target Target { get; set; }

		[DataMember(Name = "target")]
		public string TargetString
		{
			get { return Target.ToString(); }
			set { Target = (Target)Enum.Parse(typeof(Target), value); }
		}

		public Target Chooser { get; set; }

		[DataMember(Name = "chooser")]
		public string SubjectString
		{
			get { return Chooser.ToString(); }
			set { Chooser = (Target)Enum.Parse(typeof(Target), value); }
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

			switch (Chooser)
			{
				case Target.HomeBaseController:
					if (capitalize)
					{
						toStringBuilder.Append("That home base's controller chooses ");
					}
					else
					{
						toStringBuilder.Append("that home base's controller chooses ");
					}
					break;
				default:
					throw new InvalidOperationException("Unsupported Chooser for ChooseTargetEffect.");
			}

			string targetString = string.Empty;
			switch (Target)
			{
				case Target.DeviceChooserControls:
					toStringBuilder.Append("a device he or she controls");
					targetString = "a device";
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for ChooseTargetEffect.");
			}

			if ((_successEffects != null) && (_successEffects.Count > 0))
			{
				toStringBuilder.Append(". If ");
				toStringBuilder.Append(targetString);
				toStringBuilder.Append(" was chosen this way, ");

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
