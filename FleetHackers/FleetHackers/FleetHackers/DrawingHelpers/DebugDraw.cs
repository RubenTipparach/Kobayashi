using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FleetHackers.Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FleetHackers.DrawingHelpers
{
	public class DebugDraw
	{
		public static AbstractCamera _camera;
		public static LineDrawer _lineDrawer;

		public static void DrawLine(Vector3 source, Vector3 destination, Color color)
		{
			_lineDrawer.DrawLine(source, destination, color);
		}

		public static void DrawX(Vector3 source, float size, Color color)
		{
			Vector3 lineXSource = new Vector3(source.X + size/2, source.Y, source.Z);
			Vector3 lineXDestination = new Vector3(source.X - size / 2, source.Y, source.Z);

			Vector3 lineYSource = new Vector3(source.X, source.Y + size / 2, source.Z);
			Vector3 lineYDestination = new Vector3(source.X, source.Y - size / 2, source.Z);

			Vector3 lineZSource = new Vector3(source.X, source.Y, source.Z + size / 2);
			Vector3 lineZDestination = new Vector3(source.X, source.Y, source.Z - size / 2);

			_lineDrawer.DrawLine(lineXSource, lineXDestination, color);
			_lineDrawer.DrawLine(lineYSource, lineYDestination, color);
			_lineDrawer.DrawLine(lineZSource, lineZDestination, color);
		}
	}
}
