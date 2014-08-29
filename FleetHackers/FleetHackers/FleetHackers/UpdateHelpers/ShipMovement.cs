using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FleetHackers.EngineStructs;
using FleetHackers.Models;
using Microsoft.Xna.Framework;

namespace FleetHackers.UpdateHelpers
{
	public class ShipMovement
	{
		public static MovementReport MoveShip(MovementReport movementDataReporter, ref BasicModel model)
		{

			if (movementDataReporter.traveling)
			{
				Vector3 moveTowards = (movementDataReporter.newCoordinates - model.Position);
				moveTowards.Normalize();

				model.Position += moveTowards * 10;

				if (Vector3.Distance(model.Position, movementDataReporter.newCoordinates) < 5)
				{
					movementDataReporter.traveling = false;
				}
			}
			return movementDataReporter;
		}
	}
}
