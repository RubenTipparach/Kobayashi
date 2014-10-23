using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetHackers.HexagonLibrary
{
	public class HexMovement
	{
		/// <summary>
		/// Moves the in cube coordinates.
		/// </summary>
		/// <returns>The in cube coordinates.</returns>
		/// <param name="corrdinates">Corrdinates.</param>
		/// <param name="direction">Start at the left Hex, moves counter clockwise 0-5.</param>
		public CubeCoordinate MoveInCubeCoordinate(CubeCoordinate corrdinates, int direction)
		{
			Dictionary<int, int[]> neighbors = new Dictionary<int, int[]>
			{
				{0, new int[]{1, -1, 0}}, {1, new int[]{1, 0, -1}}, {2, new int[]{0, 1, -1}},
				{3, new int[]{-1, 1, 0}}, {4, new int[]{-1, 0, 1}}, {5, new int[]{0, -1, 1}}
			};

			int[] d = neighbors[direction];

			return new CubeCoordinate(corrdinates.X + d[0], corrdinates.Y + d[1], corrdinates.Z + d[2]);
		}

		/// <summary>
		/// Moves the in axial coordinates.
		/// </summary>
		/// <param name="coordinates">The coordinates.</param>
		/// <param name="direction">The direction.</param>
		/// <returns></returns>
		public AxisCoordinate MoveInAxialCoordinates(AxisCoordinate coordinates, int direction)
		{
			Dictionary<int, int[]> neighbors = new Dictionary<int, int[]>
			{
				{0, new int[]{ 1, 0}}, {1, new int[]{ 1,-1}}, {2, new int[]{ 0,-1}},
				{3, new int[]{-1, 0}}, {4, new int[]{-1, 1}}, {5, new int[]{ 0, 1}}
			};

			int[] d = neighbors[direction];

			return new AxisCoordinate(coordinates.Q + d[0], coordinates.R + d[1]);
		}

		/// <summary>
		/// Moves the in offset corrdinates.
		/// </summary>
		/// <returns>The in offset corrdinates.</returns>
		/// <param name="cooridinates">Cooridinates.</param>
		/// <param name="offsetType">Offset type.</param>
		/// <param name="direction">Direction.</param>
		public OffSetCoordinate MoveInOffsetCorrdinates(OffSetCoordinate cooridinates, OffsetCoordinateType offsetType, int direction)
		{
			Dictionary<int, Dictionary<int, int[]>> neighbors = new Dictionary<int, Dictionary<int, int[]>>();
			int parity = 0;

			if (offsetType.Equals(OffsetCoordinateType.OddR))
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

			if (offsetType.Equals(OffsetCoordinateType.EvenR))
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
			return new OffSetCoordinate(cooridinates.Q + d[0], cooridinates.R + d[1], offsetType);
		}

		/// <summary>
		/// Moves to cube diagonals.
		/// </summary>
		/// <param name="coordinates">The coordinates.</param>
		/// <param name="direction">The direction.</param>
		/// <returns></returns>
		public static CubeCoordinate MoveToCubeDiagonals(CubeCoordinate coordinates, int direction)
		{
			Dictionary<int, int[]> diagonals = new Dictionary<int, int[]>
			{
				{0, new int[]{2, -1, -1}}, {1, new int[]{1, 1, -2}}, {2, new int[]{-1,2,-1}},
				{3, new int[]{-2, 1, 1}}, {4, new int[]{-1,-1,2}}, {5, new int[]{1,-2,1}}
			};

			int[] d = diagonals[direction];
			return new CubeCoordinate(coordinates.X + d[0], coordinates.Y + d[1], coordinates.Z + d[2]);
		}
	}
}
