using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FleetHackers.Cameras
{
	public class FreeCamera : AbstractCamera
	{
		/// <summary>
		/// Translate the camera variable.
		/// </summary>
		private Vector3 translation;

		/// <summary>
		/// Main constructor.
		/// </summary>
		/// <param name="position">The position of the camera when it's created.</param>
		/// <param name="yaw">Yaw of the camera, side to side.</param>
		/// <param name="pitch">Pitch of the camera, up and down.</param>
		/// <param name="graphicsDevice">Graphics device handle.</param>
		public FreeCamera(Vector3 position, float yaw, float pitch, GraphicsDevice graphicsDevice):
			base(graphicsDevice)
		{
			this.Position = position;
			this.Yaw = yaw;
			this.Position = position;
			this.Roll = 0;

			translation = Vector3.Zero;
		}

		/// <summary>
		/// Update method required by the abstract class.
		/// This class also allows player input to control where to look.
		/// </summary>
		public override void Update()
		{
			Matrix rotation = Matrix.CreateFromYawPitchRoll(Yaw, Pitch, Roll);

			translation = Vector3.Transform(translation, rotation);
			Position += translation;
			translation = Vector3.Zero;

			Vector3 forward = Vector3.Transform(Vector3.Up, rotation);

		}

		/// <summary>
		/// Gets or sets the current position.
		/// </summary>
		public Vector3 Position
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the target of the camera.
		/// </summary>
		public Vector3 Target
		{
			get;
			set;
		}

		/// <summary>
		/// Allows the camera to look side to side.
		/// </summary>
		public float Yaw
		{
			get;
			set;
		}

		/// <summary>
		/// Allows camera to look up or down.
		/// </summary>
		public float Pitch
		{
			get;
			set;
		}

		/// <summary>
		/// Allows the camera to roll around and stuff. Probably won't use this much.
		/// </summary>
		public float Roll
		{
			get;
			set;
		}
	}
}
