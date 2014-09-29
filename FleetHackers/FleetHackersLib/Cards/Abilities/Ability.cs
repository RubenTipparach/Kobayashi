using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FleetHackersLib.Cards.Abilities
{
	[DataContract]
	[KnownType(typeof(TriggeredAbility))]
	[KnownType(typeof(ImmediateAbility))]
	[KnownType(typeof(TruefireAbility))]
	[KnownType(typeof(CloakAbility))]
	[KnownType(typeof(EntersWithCountersAbility))]
	[KnownType(typeof(ActivatedAbility))]
	[KnownType(typeof(StaticAbility))]
	[KnownType(typeof(AttachToShipAbility))]
	[KnownType(typeof(SingleAttackAbility))]
	[KnownType(typeof(AmbushAbility))]
	[KnownType(typeof(HexproofAbility))]
	[KnownType(typeof(HasteAbility))]
	[KnownType(typeof(ConditionalPlayAbility))]
	[KnownType(typeof(SecretTechAbility))]
	public abstract class Ability
	{
		public abstract AbilityType AbilityType { get; }
		public abstract string ToString(Card card);
	}
}
