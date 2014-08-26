using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FleetHackers.Cards
{
	[DataContract]
	public class StateCheck
	{
		public CheckStateType State { get; set; }

		[DataMember(Name = "state")]
		public string StateString
		{
			get
			{
				return State.ToString();
			}
			set
			{
				State = (CheckStateType)Enum.Parse(typeof(CheckStateType), value);
			}
		}

		[DataMember(Name = "test")]
		public bool Test { get; set; }
	}
}
