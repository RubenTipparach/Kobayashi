using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetHackers.HexagonLibrary
{
	public struct CubeCoordinate
	{
		public int X;
		public int Z;
		public int Y;

		public CubeCoordinate(int x, int y, int z)
		{
			X = x;
			Y = y;
			Z = z;
		}
	}
}
