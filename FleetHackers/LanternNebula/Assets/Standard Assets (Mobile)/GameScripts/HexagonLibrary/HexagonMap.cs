using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FleetHackers.HexagonLibrary
{
	public class HexagonMap
	{
		/// <summary>
		/// Converts the point coordinates to axial coordinates.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="y">The y.</param>
		/// <param name="halfWidth">Half of the width.</param>
		/// <param name="radius">The radius.</param>
		/// <returns></returns>
		public static AxisCoordinate ConvertPointCoordToAxialCoord(float x, float y, float halfWidth, float radius)
		{
			x = (x - halfWidth) / (halfWidth * 2.0f);
			float temp1 = y/radius;
			float temp2 = Mathf.Floor(x + temp1);
			float r = Mathf.Floor((Mathf.Floor(temp1 - x) + temp2) / 3.0f);
			float q = Mathf.Floor((Mathf.Floor( 2.0f * x + 1.0f) + temp2) / 3.0f) - r;

			return new AxisCoordinate((int)q, (int)r);
		}
	}


	

	

	


}
