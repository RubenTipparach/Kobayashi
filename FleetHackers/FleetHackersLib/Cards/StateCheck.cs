using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackersLib.Cards.Enums;

namespace FleetHackersLib.Cards
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
