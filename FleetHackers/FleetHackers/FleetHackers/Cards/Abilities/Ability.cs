﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FleetHackers.Cards.Abilities
{
	[DataContract]
	[KnownType(typeof(TriggeredAbility))]
	[KnownType(typeof(ImmediateAbility))]
	[KnownType(typeof(TruefireAbility))]
	[KnownType(typeof(CloakAbility))]
	public abstract class Ability
	{
		public abstract AbilityType AbilityType { get; }
		public abstract string ToString(Card card);
	}
}
