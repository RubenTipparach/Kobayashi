using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FleetHackers.EngineStructs
{
	/// <summary>
	/// Add any global variables that are manipulated in the main game class.
	/// </summary>
	public struct MovementReport
	{
		/// <summary>
		/// The traveling.
		/// </summary>
		public bool traveling;

		/// <summary>
		/// The new coordinates.
		/// </summary>
		public Vector3 newCoordinates;

		public Dictionary<string, bool> PlayerShipSelected;
	}
}
