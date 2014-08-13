using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FleetHackers.Cameras
{
	public class TargetCamera : AbstractCamera
	{
		/// <summary>
		/// Default constructor for the target camera.
		/// </summary>
		/// <param name="graphicsDevice">Graphics device object.</param>
		public TargetCamera(Vector3 position, Vector3 target, GraphicsDevice graphicsDevice)
			: base(graphicsDevice)
		{
			this.Position = position;
			this.Target = target;
		}

		/// <summary>
		/// Synchronize this camera update with the rest of the game.
		/// </summary>
		public override void Update()
		{
			Vector3 forward = Target - Position;
			Vector3 side = Vector3.Cross(forward, Vector3.Up);
			Vector3 up = Vector3.Cross(side, forward);
			this.View = Matrix.CreateLookAt(Position, Target, up);
		}

		/// <summary>
		/// Position of the camera.
		/// </summary>
		public Vector3 Position
		{
			get;
			set;
		}

		/// <summary>
		/// The target for this camera.
		/// </summary>
		public Vector3 Target
		{
			get;
			set;
		}
	}
}
