using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FleetHackers.Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FleetHackers.Input
{
	/// <summary>
	/// 
	/// </summary>
	public class RayPick
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RayPick"/> class.
		/// </summary>
		public RayPick()
		{
		}

		/// <summary>
		/// Gets the pick ray. Single use, probably make static for now....
		/// </summary>
		/// <returns></returns>
		public static Ray GetPickRay(AbstractCamera camera, GraphicsDevice graphicsDevice)
		{
			MouseState mouseState = Mouse.GetState();

			// Reminder that these are clipping planes. The ray goes from the camera outwards.
			Vector3 nearSource = new Vector3((float)mouseState.X, mouseState.Y, 0f);
			Vector3 farSource = new Vector3((float)mouseState.X, mouseState.Y, 1f);

			Matrix world = Matrix.CreateTranslation(0, 0, 0);

			Vector3 nearPoint = graphicsDevice.Viewport.Unproject(nearSource, camera.Projection, camera.View, world);
			Vector3 farPoint = graphicsDevice.Viewport.Unproject(farSource, camera.Projection, camera.View, world);

			// Direction is thease two points make up a line.
			Vector3 direction = farPoint - nearPoint;
			direction.Normalize();

			return new Ray(nearPoint, direction);
		}
	}
}
