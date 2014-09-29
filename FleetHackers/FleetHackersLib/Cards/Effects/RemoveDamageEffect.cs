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
	public class RemoveDamageEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.RemoveDamage;
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


		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (capitalize)
			{
				toStringBuilder.Append("Remove ");
			}
			else
			{
				toStringBuilder.Append("remove ");
			}

			toStringBuilder.Append("any damage that was inflicted to ");

			switch (Target)
			{
				case Target.AttachedShip:
					toStringBuilder.Append("attached ship");
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for RemoveDamageEffect.");
			}

			toStringBuilder.Append(" this turn");

			return toStringBuilder.ToString();
		}
	}
}
