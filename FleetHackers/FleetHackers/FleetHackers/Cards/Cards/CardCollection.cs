using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;

namespace FleetHackers.Cards
{
	[DataContract]
	public class CardCollection : List<Card>
	{
		public CardCollection(IEnumerable<Card> collection) : base(collection) { }

		public static CardCollection Deserialize(string json)
		{
			List<Card> obj = Activator.CreateInstance<List<Card>>();
			using (var memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
			{
				var serializer = new DataContractJsonSerializer(obj.GetType());
				obj = (List<Card>)serializer.ReadObject(memoryStream);
				return new CardCollection(obj);
			}
		}
	}
}
