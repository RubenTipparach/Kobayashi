using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackersLib.Cards.Effects.Enums;
using FleetHackersLib.Cards.Enums;

namespace FleetHackersLib.Cards.Effects
{
	[DataContract]
	public class MicrodroneEffect : Effect
	{
		public override EffectType EffectType
		{
			get
			{
				return EffectType.Microdrone;
			}
		}

		[DataMember(Name = "optional")]
		public bool Optional { get; set; }

		[DataMember(Name = "count")]
		public int Count { get; set; }

		public override string ToString(Card card, bool capitalize = false)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (Optional)
			{
				if (capitalize)
				{
					toStringBuilder.Append("You may put ");
				}
				else
				{
					toStringBuilder.Append("you may put ");
				}
			}
			else
			{
				if (capitalize)
				{
					toStringBuilder.Append("Put ");
				}
				else
				{
					toStringBuilder.Append("put ");
				}
			}

			if (Count == 1)
			{
				toStringBuilder.Append("a 1/1 xeno Microdrone ship token ");
			}
			else
			{
				toStringBuilder.Append(Count.ToString());
				toStringBuilder.Append(" 1/1 xeno Microdrone ship tokens ");
			}

			toStringBuilder.Append("into the battle zone");

			return toStringBuilder.ToString();
		}
	}
}
