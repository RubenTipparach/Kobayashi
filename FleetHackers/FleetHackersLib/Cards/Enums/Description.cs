using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace FleetHackersLib.Cards.Enums
{
	public class Description : Attribute
	{
		public string Text { get; set; }

		public Description(string text)
		{
			Text = text;
		}

		public static string ToDescription(Enum en)
		{
			Type type = en.GetType();
			MemberInfo[] memInfo = type.GetMember(en.ToString());
			if (memInfo != null && memInfo.Length > 0)
			{
				object[] attrs = memInfo[0].GetCustomAttributes(typeof(Description), false);
				if (attrs != null && attrs.Length > 0)
				{
					return ((Description)attrs[0]).Text;
				}
			}

			return en.ToString();
		}
	}
}
