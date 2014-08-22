using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace FleetHackers.Cards.Abilities
{
	public class Trigger
	{
		public TriggerType TriggerType { get; set; }

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

		public Target Actor { get; set; }

		public string ToString(Card card)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			switch (Actor)
			{
				case Target.OpponentShip:
					toStringBuilder.Append("a ship controlled by an opponent ");
					break;
				default:
					throw new InvalidOperationException("Unsupported Actor for Trigger.");
			}

			switch (TriggerType)
			{
				case TriggerType.Attack:
					toStringBuilder.Append("attacks ");
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
					default:
						throw new InvalidOperationException("Unsupported Target for Trigger.");
				}
			}
			toStringBuilder.Append(string.Join(" or ", targetStrings));

			return toStringBuilder.ToString();
		}
	}
}
