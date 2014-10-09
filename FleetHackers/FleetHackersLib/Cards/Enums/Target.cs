using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetHackersLib.Cards.Enums
{
	public enum Target
	{
		None,
		YourShip,
		YourHomeBase,
		OpponentShip,
		OpponentHomeBase,
		AttackingShips,
		This,
		AnyShip,
		UpTo2OtherShips,
		AnyDevice,
		OtherDevice,
		InterceptingShip,
		NonInfluence,
		YourChargedShips,
		Opponent,
		AttachedShip,
		TargettedShip,
		OtherShipYouControl,
		You,
		YourNonInfluence,
		TargetShipController,
		AttackingShip,
		AnySource,
		ThatShip,
		DamagedShip,
		AnyHomeBase,
		ThatPlayer,
		HomeBaseController,
		DeviceChooserControls,
		ChosenDevice,
		ExhaustedShip,
		OtherShipsOfType,
		NextCardYouPlayThisTurn,
		InterceptedShip,
		ThatAbility,
		DefendingShip,
		YourRandomShip
	}
}
