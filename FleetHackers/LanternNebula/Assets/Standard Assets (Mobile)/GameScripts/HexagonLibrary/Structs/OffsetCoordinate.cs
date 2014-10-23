using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetHackers.HexagonLibrary
{
	public struct OffSetCoordinate
	{
		public int Q;
		public int R;
		public OffsetCoordinateType OffsetType;

		public OffSetCoordinate(int q, int r, OffsetCoordinateType offsetType)
		{
			Q = q;
			R = r;
			OffsetType = offsetType;
		}

		public override string ToString()
		{
			return string.Format("Q: {0} R: {1} OffsetType: {2}", Q, R, OffsetType);
		}
	}
}
