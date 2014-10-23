using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetHackers.HexagonLibrary
{
	class HexDistance
	{

		/// <summary>
		/// Caclulates the cube hexagon distance.
		/// </summary>
		/// <param name="cube1">The cube1.</param>
		/// <param name="cube2">The cube2.</param>
		/// <returns></returns>
		public static float CaclulateCubeCoordDistance(CubeCoordinate cube1, CubeCoordinate cube2)
		{
			return (float)(Math.Abs(cube1.X - cube2.X) + Math.Abs(cube1.Y - cube1.Y) + Math.Abs(cube1.Z - cube2.Z) / 2);
		}

		public static float CalculateAxialCoordDistance(AxisCoordinate axis1, AxisCoordinate axis2)
		{
			return 1;
		}
	}
}
