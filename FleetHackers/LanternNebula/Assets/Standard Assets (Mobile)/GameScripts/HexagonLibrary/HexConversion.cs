using UnityEngine;
using System.Collections;

namespace FleetHackers.HexagonLibrary
{
	public class HexConversion
	{
		#region Cube to X Conversion

		/// <summary>
		/// Converts the cube to axial.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="z">The z.</param>
		/// <returns></returns>
		public static AxisCoordinate CubeToAxial(CubeCoordinate c)
		{
			int q = c.X;
			int r = c.Z;
			return new AxisCoordinate(q, r);
		}

		/// <summary>
		/// Converts the cube to even q.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="z">The z.</param>
		/// <returns></returns>
		public static OffSetCoordinate CubeToEvenQ(CubeCoordinate c)
		{
			int q = c.X;
			int r = c.Z + (c.X + (c.X & 1)) / 2; // 0 means even, 1 odd Bitwise op.
			return new OffSetCoordinate(q, r, OffsetCoordinateType.EvenQ);
		}

		/// <summary>
		/// Converts the cube to odd q.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="z">The z.</param>
		/// <returns></returns>
		public static OffSetCoordinate CubeToOddQ(CubeCoordinate c)
		{
			int q = c.X;
			int r = c.Z + (c.X - (c.X & 1)) / 2;
			return new OffSetCoordinate(q, r, OffsetCoordinateType.OddR);
		}

		/// <summary>
		/// Converts the cube to even r.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="z">The z.</param>
		/// <returns></returns>
		public static OffSetCoordinate CubeToEvenR(CubeCoordinate c)
		{
			int q = c.X + (c.Z + (c.Z & 1)) / 2;
			int r = c.Z;
			return new OffSetCoordinate(q, r, OffsetCoordinateType.EvenR);
		}

		/// <summary>
		/// Converts the cube to odd r.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="z">The z.</param>
		/// <returns></returns>
		public static OffSetCoordinate CubeToOddR(CubeCoordinate c)
		{
			int q = c.X + (c.Z - (c.Z & 1)) / 2;
			int r = c.Z;
			return new OffSetCoordinate(q, r, OffsetCoordinateType.OddR);
		}

		#endregion

		#region X to Cube Convertion

		/// <summary>
		/// Converts the axial to cube.
		/// </summary>
		/// <param name="q">The q.</param>
		/// <param name="r">The r.</param>
		/// <returns></returns>
		public static CubeCoordinate AxialToCube(OffSetCoordinate o)
		{
			int x = o.Q;
			int z = o.R;
			int y = -x - z;
			return new CubeCoordinate(x, y, z);
		}

		/// <summary>
		/// Converts the odd q to cube.
		/// </summary>
		/// <param name="q">The q.</param>
		/// <param name="r">The r.</param>
		/// <returns></returns>
		public static CubeCoordinate OddQToCube(OffSetCoordinate o)
		{
			int x = o.Q;
			int z = o.R - (o.Q - (o.Q & 1)) / 2;
			int y = -x - z;
			return new CubeCoordinate(x, y, z);
		}

		/// <summary>
		/// Converts the odd r to cube.
		/// </summary>
		/// <param name="q">The q.</param>
		/// <param name="r">The r.</param>
		/// <returns></returns>
		public static CubeCoordinate OddRToCube(OffSetCoordinate o)
		{
			int x = o.Q - (o.R - (o.R & 1)) / 2;
			int z = o.R;
			int y = -x - z;
			return new CubeCoordinate(x, y, z);
		}

		/// <summary>
		/// Converts the even r to cube.
		/// </summary>
		/// <param name="q">The q.</param>
		/// <param name="r">The r.</param>
		/// <returns></returns>
		public static CubeCoordinate EvenRToCube(OffSetCoordinate o)
		{
			int x = o.Q - (o.R + (o.R & 1)) / 2;
			int z = o.R;
			int y = -x - z;
			return new CubeCoordinate(x, y, z);
		}

		/// <summary>
		/// Converts the even q to cube.
		/// </summary>
		/// <param name="q">The q.</param>
		/// <param name="r">The r.</param>
		/// <returns></returns>
		public static CubeCoordinate EvenQToCube(OffSetCoordinate o)
		{
			int x = o.Q;
			int z = o.R - (o.Q - (o.Q & 1)) / 2;
			int y = -x - z;
			return new CubeCoordinate(x, y, z);
		}

		#endregion
	}
}
