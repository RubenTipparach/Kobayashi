using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FleetHackers.DrawingHelpers
{
	public class HexagonCoordinates
	{
		#region Conversion stuff
		public static Vector2 ConvertCubeToAxial(int x, int z)
		{
			int q = x;
			int r = z;
			return new Vector2(q, r);
		}

		public static Vector3 ConvertAxialToCube(int q, int r)
		{
			int x = q;
			int z = r;
			int y = -x - z;
			return new Vector3(x, y, z);
		}

		public static Vector2 ConvertCubeToEvenQ(int x, int z)
		{
			int q = x;
			int r = z + (x + (x & 1)) / 2; // 0 means even, 1 odd Bitwise op.
			return new Vector2(q, r);
		}

		public static Vector3 ConvertEvenQToCube(int q, int r)
		{
			int x = q;
			int z = r - (q - (q & 1)) / 2;
			int y = -x - z;
			return new Vector3(x, y, z);
		}

		public static Vector2 ConvertCubeToOddQ(int x, int z)
		{
			int q = x;
			int r = z + (x - (x & 1)) / 2;
			return new Vector2(q, r);
		}

		public static Vector3 ConvertOddQToCube(int q, int r)
		{
			int x = q;
			int z = r - (q - (q & 1)) / 2;
			int y = -x - z;
			return new Vector3(x, y, z);
		}

		public static Vector2 ConvertCubeToEvenR(int x, int z)
		{
			int q = x + (z + (z & 1)) / 2;
			int r = z;
			return new Vector2(q, r);
		}

		public static Vector3 ConvertEvenRToCube(int q, int r)
		{
			int x = q - (r + (r & 1)) / 2;
			int z = r;
			int y = -x - z;
			return new Vector3(x, y, z);
		}

		public static Vector2 ConvertCubeToOddR(int x, int z)
		{
			int q = x + (z - (z & 1)) / 2;
			int r = z;
			return new Vector2(q, r);
		}

		public static Vector3 ConvertOddRToCube(int q, int r)
		{
			int x = q - (r - (r & 1)) / 2;
			int z = r;
			int y = -x - z;
			return new Vector3(x, y, z);
		}
		#endregion

		#region Move around one step.
		public CubeCoordinates MoveInCubeCoordinates(CubeCoordinates corrdinates,int direction)
		{
			Dictionary<int, int[]> neighbors = new Dictionary<int, int[]>
			{
				{0, new int[]{1, -1, 0}}, {1, new int[]{1, 0, -1}}, {2, new int[]{0, 1, -1}},
				{3, new int[]{-1, 1, 0}}, {4, new int[]{-1, 0, 1}}, {5, new int[]{0, -1, 1}}
			};

			int[] d = neighbors[direction];

			return new CubeCoordinates(corrdinates.X + d[0], corrdinates.Y + d[1], corrdinates.Z + d[2]);
		}

		public AxisCoordinates MoveInAxialCoordinates(AxisCoordinates coordinates, int direction)
		{
			Dictionary<int, int[]> neighbors = new Dictionary<int, int[]>
			{
				{0, new int[]{ 1, 0}}, {1, new int[]{ 1,-1}}, {2, new int[]{ 0,-1}},
				{3, new int[]{-1, 0}}, {4, new int[]{-1, 1}}, {5, new int[]{ 0, 1}}
			};

			int[] d = neighbors[direction];

			return new AxisCoordinates(coordinates.Q + d[0], coordinates.R + d[1]);
		}

		public OffSetCoordinates MoveInOffsetCorrdinates(OffSetCoordinates cooridinates, OffsetCoordinateType offsetType, int direction)
		{
			Dictionary<int, Dictionary<int, int[]>> neighbors = new Dictionary<int, Dictionary<int, int[]>>();
			int parity = 0;

			if(offsetType.Equals(OffsetCoordinateType.OddR))
			{
				neighbors = new Dictionary<int, Dictionary<int, int[]>>
				{
					{0, new Dictionary<int, int[]>
						{
							{0, new int[]{ 1, 0}}, {1, new int[]{0, -1}}, {2, new int[]{-1, -1}},
							{3, new int[]{-1, 0}}, {4, new int[]{-1, 1}}, {5, new int[]{ 0,  1}}
						}
					},
					{1, new Dictionary<int, int[]>
						{
							{0, new int[]{ 1, 0}}, {1, new int[]{ 1, -1}}, {2, new int[]{ 0, -1}},
							{3, new int[]{-1, 0}}, {4, new int[]{ 0,  1}}, {5, new int[]{ 1,  1}}
						}
					}
				};
				parity = cooridinates.R & 1;
			}

			if(offsetType.Equals(OffsetCoordinateType.EvenR))
			{
				neighbors = new Dictionary<int, Dictionary<int, int[]>>
				{
					{0, new Dictionary<int, int[]>
						{
							{0, new int[]{ 1, 0}}, {1, new int[]{ 1, -1}}, {2, new int[]{ 0, -1}},
							{3, new int[]{-1, 0}}, {4, new int[]{ 0,  1}}, {5, new int[]{ 1,  1}}
						}
					},				
					{1, new Dictionary<int, int[]>
						{
							{0, new int[]{ 1, 0}}, {1, new int[]{0, -1}}, {2, new int[]{-1, -1}},
							{3, new int[]{-1, 0}}, {4, new int[]{-1, 1}}, {5, new int[]{ 0,  1}}
						}
					}
				};
				parity = cooridinates.R & 1;
			}

			if (offsetType.Equals(OffsetCoordinateType.OddQ))
			{
				neighbors = new Dictionary<int, Dictionary<int, int[]>>
				{
					{0, new Dictionary<int, int[]>
						{
							{0, new int[]{ 1,  0}}, {1, new int[]{ 1, -1}}, {2, new int[]{ 0, -1}},
							{3, new int[]{-1, -1}}, {4, new int[]{-1,  0}}, {5, new int[]{ 0,  1}}
						}
					},				
					{1, new Dictionary<int, int[]>
						{
							{0, new int[]{ 1, 1}}, {1, new int[]{ 1, 0}}, {2, new int[]{ 0, -1}},
							{3, new int[]{-1, 0}}, {4, new int[]{-1, 1}}, {5, new int[]{ 0,  1}}
						}
					}
				};
				parity = cooridinates.Q & 1;
			}

			if (offsetType.Equals(OffsetCoordinateType.EvenQ))
			{
				neighbors = new Dictionary<int, Dictionary<int, int[]>>
				{
					{0, new Dictionary<int, int[]>
						{
							{0, new int[]{ 1, 1}}, {1, new int[]{ 1, 0}}, {2, new int[]{ 0, -1}},
							{3, new int[]{-1, 0}}, {4, new int[]{-1, 1}}, {5, new int[]{ 0,  1}}
						}
					},				
					{1, new Dictionary<int, int[]>
						{
							{0, new int[]{ 1, 0}}, {1, new int[]{ 1,-1}}, {2, new int[]{ 0, -1}},
							{3, new int[]{-1,-1}}, {4, new int[]{-1, 0}}, {5, new int[]{ 0,  1}}
						}
					}
				};
				parity = cooridinates.Q & 1;
			}

			int[] d = neighbors[parity][direction];
			return new OffSetCoordinates(cooridinates.Q + d[0], cooridinates.R + d[1]);
		}
		#endregion

		#region Coordinate structs
		public struct CubeCoordinates
		{
			public int X;
			public int Z;
			public int Y;

			public CubeCoordinates(int x, int y, int z)
			{
				X = x;
				Y = y;
				Z = z;
			}
		}

		public struct AxisCoordinates
		{
			public int Q;
			public int R;

			public AxisCoordinates(int q, int r)
			{
				Q = q;
				R = r;
			}
		}

		public struct OffSetCoordinates
		{
			public int Q;
			public int R;

			public OffSetCoordinates(int q, int r)
			{
				Q = q;
				R = r;
			}
		}

		public enum OffsetCoordinateType
		{
			OddR,
			EvenR,
			OddQ,
			EvenQ
		}
		#endregion

		public static CubeCoordinates MoveToCubeDiagonals(CubeCoordinates coordinates, int direction)
		{
			Dictionary<int, int[]> diagonals = new Dictionary<int, int[]>
			{
				{0, new int[]{2, -1, -1}}, {1, new int[]{1, 1, -2}}, {2, new int[]{-1,2,-1}},
				{3, new int[]{-2, 1, 1}}, {4, new int[]{-1,-1,2}}, {5, new int[]{1,-2,1}}
			};

			int[] d = diagonals[direction];
			return new CubeCoordinates(coordinates.X + d[0], coordinates.Y + d[1], coordinates.Z + d[2]);
		}

		#region HexagonDistances

		public static float CubeHexagonDistance(CubeCoordinates cube1, CubeCoordinates cube2)
		{
			return (float)(Math.Abs(cube1.X - cube2.X) + Math.Abs(cube1.Y - cube1.Y) + Math.Abs(cube1.Z - cube2.Z)/2);
		}

		#endregion
	}
}
