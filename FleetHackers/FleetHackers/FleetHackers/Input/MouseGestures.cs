using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FleetHackers.Cameras;
using FleetHackers.EngineStructs;
using FleetHackers.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FleetHackers.Input
{
	/// <summary>
	/// 
	/// </summary>
	public class MouseGestures
	{

		/// <summary>
		/// The last mouse position.
		/// </summary>
		private static Vector2 _lastMousePosition;

		/// <summary>
		/// Initializes a new instance of the <see cref="MouseGestures"/> class.
		/// </summary>
		public MouseGestures()
		{
		}

		/// <summary>
		/// Checks the mouse clicked.
		/// </summary>
		/// <param name="models">The models.</param>
		/// <param name="boardPlane">The board plane.</param>
		/// <param name="mouseDataReporter">The mouse data reporter.</param>
		/// <param name="camera">The camera.</param>
		/// <param name="graphicsDevice">The graphics device.</param>
		/// <returns></returns>
		public static MovementReport CheckMouseClicked(List<BasicModel> models, Plane boardPlane, MovementReport mouseDataReporter, AbstractCamera camera, GraphicsDevice graphicsDevice)
		{
			MouseState mouseState = Mouse.GetState();

			if (mouseState.LeftButton == ButtonState.Pressed)
			{
				Ray pickRay = RayPick.GetPickRay(camera, graphicsDevice);

				// Ray collides with a model.
				foreach (BasicModel model in models)
				{
					Nullable<float> result = pickRay.Intersects(model.BoundingSphere);
					float selectedDistance = float.MaxValue;

					if (result.HasValue)
					{
						selectedDistance = result.Value;

						Console.WriteLine(string.Format("Slected ship, {0} units away.", selectedDistance));
						return mouseDataReporter;
					}
				}

				// Ray collides with the game board's plane.
				float collisionAt = float.MaxValue;
				Nullable<float> boardCollision = pickRay.Intersects(boardPlane);

				if (boardCollision.HasValue)
				{
					collisionAt = boardCollision.Value;
					Vector3 pointOfContact = pickRay.Position + pickRay.Direction * boardCollision.Value;
					
					mouseDataReporter.traveling = true;
					mouseDataReporter.newCoordinates = pointOfContact;

					Console.WriteLine(string.Format("Slected board space, {0} units away. At position {1}", collisionAt, pointOfContact));
					return mouseDataReporter;
				}
			}

			return mouseDataReporter;
		}

		/// <summary>
		/// Rights the mouse down. We're going to use this to pan around the camera.
		/// </summary>
		public static Vector3 RightMouseDown(float moveSpeed, Nullable<Plane> boardPlane = null, GraphicsDevice graphicsDevice = null)
		{
			Vector3 deltaPosition = Vector3.Zero;

			MouseState mouseState = Mouse.GetState();
			Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);

			if (mouseState.RightButton == ButtonState.Pressed || mouseState.MiddleButton.Equals(ButtonState.Pressed))
			{
				Vector2 delta = mousePosition - _lastMousePosition;
				deltaPosition += new Vector3(delta.X, 0, delta.Y) * -(float)moveSpeed;
			}

			_lastMousePosition = mousePosition;

			return deltaPosition;
		}
	}
}
