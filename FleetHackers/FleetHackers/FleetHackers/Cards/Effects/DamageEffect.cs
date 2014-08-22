using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace FleetHackers.Cards.Effects
{
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

		public Target Actor { get; set; }
		public int Amount { get; set; }
		public bool Exact { get; set; }
		public DivideMethod DivideMethod { get; set; }

		public override string ToString(Card card)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			switch (Actor)
			{
				case Target.This:
					toStringBuilder.Append(card.Title);
					toStringBuilder.Append(" ");
					break;
				default:
					throw new InvalidOperationException("Unsupported Actor for DamageEffect.");
			}

			toStringBuilder.Append("inflicts ");

			if (!Exact)
			{
				toStringBuilder.Append("up to ");
			}

			toStringBuilder.Append(Amount.ToString());
			toStringBuilder.Append(" damage ");

			switch (DivideMethod)
			{
				case DivideMethod.YouChooseAny:
					toStringBuilder.Append("divided however you choose among ");
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
					default:
						throw new InvalidOperationException("Unsupported Target for DamageEffect.");
				}
			}
			toStringBuilder.Append(string.Join(" or ", targetStrings));

			return toStringBuilder.ToString();
		}
	}
}
