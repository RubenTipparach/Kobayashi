using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FleetHackersLib.Cards.Enums;
using System.Runtime.Serialization;

namespace FleetHackersLib.Cards.Effects.Conditions
{
	[DataContract]
	public class EffectCondition
	{
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

		public ActivePlayerAttribute ActivePlayerAttribute { get; set; }

		[DataMember(Name = "activePlayerAttribute")]
		public string ActivePlayerAttributeString
		{
			get
			{
				return ActivePlayerAttribute.ToString();
			}
			set
			{
				ActivePlayerAttribute = (ActivePlayerAttribute)Enum.Parse(typeof(ActivePlayerAttribute), value);
			}
		}

		public Comparison Comparison { get; set; }

		[DataMember(Name = "comparison")]
		public string ComparisonString
		{
			get
			{
				return Comparison.ToString();
			}
			set
			{
				Comparison = (Comparison)Enum.Parse(typeof(Comparison), value);
			}
		}

		public AmountType ValueType { get; set; }

		[DataMember(Name = "valueType")]
		public string ValueTypeString
		{
			get
			{
				return ValueType.ToString();
			}
			set
			{
				ValueType = (AmountType)Enum.Parse(typeof(AmountType), value);
			}
		}

		public Variable ValueVar { get; set; }

		[DataMember(Name = "valueVar")]
		public string ValueVarString
		{
			get
			{
				return ValueVar.ToString();
			}
			set
			{
				ValueVar = (Variable)Enum.Parse(typeof(Variable), value);
			}
		}

		[DataMember(Name = "value")]
		public int Value { get; set; }

		public override string ToString()
		{
			StringBuilder toStringBuilder = new StringBuilder();

			if (Attribute != CardAttribute.None)
			{
				toStringBuilder.Append("with ");

				toStringBuilder.Append(Description.ToDescription(Attribute));

				toStringBuilder.Append(" of ");


				if (ValueType == AmountType.Numeric)
				{
					toStringBuilder.Append(Value.ToString());
				}
				else
				{
					toStringBuilder.Append(Description.ToDescription(ValueVar));
				}

				switch (Comparison)
				{
					case Comparison.LessThanOrEqual:
						toStringBuilder.Append(" or less");
						break;
					default:
						throw new InvalidOperationException("Unsupported Comparison for EffectCondition.");
				}
			}
			else
			{
				string plural = string.Empty;
				string comparisonString = string.Empty;

				if (!(Comparison == Comparison.IsEven || Comparison == Comparison.IsOdd))
				{
					if (ValueType == AmountType.Numeric)
					{
						if (Value != 1)
						{
							plural = "s";
						}
						comparisonString = Value.ToString();
					}
					else
					{
						plural = "s";
						comparisonString = Description.ToDescription(ValueVar);
					}
				}

				switch (Comparison)
				{
					case Comparison.IsEven:
						comparisonString = "an even number of";
						break;
					case Comparison.GreaterThanOrEqual:
						if ((ValueType == AmountType.Numeric) && (Value == 1))
						{
							comparisonString = "a";
						}
						else
						{
							comparisonString = "at least " + comparisonString;
						}
						break;
					default:
						throw new InvalidOperationException("Unsupported Comparison for EffectCondition.");
				}

				toStringBuilder.Append(string.Format(Description.ToDescription(ActivePlayerAttribute), comparisonString, plural));
			}

			return toStringBuilder.ToString();
		}
	}
}
