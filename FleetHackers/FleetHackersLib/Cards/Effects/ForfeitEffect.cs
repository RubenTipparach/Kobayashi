using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackersLib.Cards.Effects.Enums;
using FleetHackersLib.Cards.Enums;
using FleetHackersLib.Cards.AlternateCosts;
using System.Collections.ObjectModel;

namespace FleetHackersLib.Cards.Effects
{
	[DataContract]
	public class ForfeitEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.Forfeit;
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

		[DataMember(Name = "alternateCosts")]
		private readonly List<AlternateCost> _alternateCosts = new List<AlternateCost>();
		private ReadOnlyCollection<AlternateCost> _alternateCostsView;
		public ReadOnlyCollection<AlternateCost> AlternateCosts
		{
			get
			{
				if (_alternateCosts == null)
				{
					return null;
				}
				if (_alternateCostsView == null)
				{
					_alternateCostsView = new ReadOnlyCollection<AlternateCost>(_alternateCosts);
				}
				return _alternateCostsView;
			}
		}

		[DataMember(Name = "AlternateEnergyCost")]
		public int AlternateEnergyCost { get; set; }

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (capitalize)
			{
				toStringBuilder.Append("Forfeit ");
			}
			else
			{
				toStringBuilder.Append("forfeit ");
			}

			switch (Target)
			{
				case Target.This:
					toStringBuilder.Append(card.Title);
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for ForfeitEffect.");
			}

			if (((_alternateCosts != null) && (_alternateCosts.Count > 0)) || (AlternateEnergyCost > 0))
			{
				toStringBuilder.Append(" unless you ");

				List<string> alternateCostStrings = new List<string>();
				if (AlternateEnergyCost > 0)
				{
					alternateCostStrings.Add(string.Format("pay {{{0}}}", AlternateEnergyCost));
				}
				if ((_alternateCosts != null) && (_alternateCosts.Count > 0))
				{
					foreach (AlternateCost cost in _alternateCosts)
					{
						alternateCostStrings.Add(cost.ToString(card, false));
					}
				}
				toStringBuilder.Append(string.Join(" and ", alternateCostStrings));
			}
			
			return toStringBuilder.ToString();
		}
	}
}
