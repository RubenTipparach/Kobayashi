using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetHackers.HexagonLibrary
{
	public struct AxisCoordinate
	{
		public int Q;
		public int R;

		public AxisCoordinate(int q, int r)
		{
			Q = q;
			R = r;
		}

		public override string ToString()
		{
			return string.Format("Q: {0} R: {1}", Q, R);
		}
	}
}
