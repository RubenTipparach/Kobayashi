﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using FleetHackersLib.Cards;
using System.Diagnostics;

namespace FleetHackersServer
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
	public class FleetHackersService : IFleetHackersService
	{
		public string GetData(int value)
		{
			return string.Format("You entered: {0}", value);
		}

		public CompositeType GetDataUsingDataContract(CompositeType composite)
		{
			if (composite == null)
			{
				throw new ArgumentNullException("composite");
			}
			if (composite.BoolValue)
			{
				composite.StringValue += "Suffix";
			}
			return composite;
		}

		public List<Card> GetCardData(List<Card> clientCards)
		{
			//// TEST DESERIALIZATION
			//List<Card> cards = clientCards; ;
			//foreach (Card c in cards)
			//{
			//    Debug.WriteLine(c.Title);
			//    Debug.WriteLine(c.RulesText);
			//}

			return clientCards;
		}
	}
}
