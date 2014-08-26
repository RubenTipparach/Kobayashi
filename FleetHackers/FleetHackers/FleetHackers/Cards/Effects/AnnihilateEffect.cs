using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using System.Reflection;

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

		[DataMember(Name = "optional")]
		public bool Optional { get; set; }

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

			switch (Target)
			{
				case Target.AnyDevice:
					toStringBuilder.Append("target device");
					break;
				case Target.OtherDevice:
					toStringBuilder.Append("another target device");
					break;
				default:
					throw new InvalidOperationException("Unsupported Target for AnnihilateEffect.");
			}

			return toStringBuilder.ToString();
		}
	}
}
