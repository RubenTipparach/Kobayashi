using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackersLib.Cards.Enums;

namespace FleetHackersLib.Cards
{
	[DataContract]
	public class VariableBinding
	{
		public Variable Variable { get; set; }

		[DataMember(Name = "variable")]
		public string VariableString
		{
			get
			{
				return Variable.ToString();
			}
			set
			{
				Variable = (Variable)Enum.Parse(typeof(Variable), value);
			}
		}

		public CardAttribute Attribute { get; set; }

		[DataMember(Name = "attribute")]
		public string AttributeString
		{
			get
			{
				return Attribute.ToString();
			}
			set
			{
				Attribute = (CardAttribute)Enum.Parse(typeof(CardAttribute), value);
			}
		}

		public ValueModifier ValueModifier { get; set; }

		[DataMember(Name = "valueModifier")]
		public string ValueModifierString
		{
			get
			{
				return ValueModifier.ToString();
			}
			set
			{
				ValueModifier = (ValueModifier)Enum.Parse(typeof(ValueModifier), value);
			}
		}
	}
}
