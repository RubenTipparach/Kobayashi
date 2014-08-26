using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FleetHackers.Cameras
{
	public class FreeCamera : AbstractCamera
	{
		/// <summary>
		/// Translate the camera variable.
		/// </summary>
		private Vector3 _translation;

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
			this.Pitch = pitch;
			this.Roll = 0;

			_translation = Vector3.Zero;
		}

		/// <summary>
		/// Update method required by the abstract class.
		/// This class also allows player input to control where to look.
		/// </summary>
		public override void Update()
		{
			Matrix rotation = Matrix.CreateFromYawPitchRoll(Yaw, Pitch, Roll);

			_translation = Vector3.Transform(_translation, rotation);
			Position += _translation;
			_translation = Vector3.Zero;

			Vector3 forward = Vector3.Transform(Vector3.Forward, rotation);
			Target = Position + forward;

			Vector3 up = Vector3.Transform(Vector3.Up, rotation);

			View = Matrix.CreateLookAt(Position, Target, up);

			this.Up = up;
			this.Right = Vector3.Cross(forward, up);
		}

		/// <summary>
		/// Controls the behavoir of the Free camera.
		/// TODO: Move input logic to Free Camera class.
		/// </summary>
		public void FreeCameraUpdate(GameTime gameTime, AbstractCamera camera, MouseState lastMouseState)
		{
			MouseState mouseState = Mouse.GetState();
			KeyboardState keyState = Keyboard.GetState();

			float deltaX = (float)lastMouseState.X - (float)mouseState.X;
			float deltaY = (float)lastMouseState.Y - (float)mouseState.Y;

			((FreeCamera)camera).Rotate(deltaX * .01f, deltaY * .01f);

			Vector3 translation = Vector3.Zero;

			if (keyState.IsKeyDown(Keys.W)) translation += Vector3.Forward;
			if (keyState.IsKeyDown(Keys.S)) translation += Vector3.Backward;
			if (keyState.IsKeyDown(Keys.A)) translation += Vector3.Left;
			if (keyState.IsKeyDown(Keys.D)) translation += Vector3.Right;

			translation *= 10 * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

			((FreeCamera)camera).Move(translation);

			camera.Update();

			lastMouseState = mouseState;
		}


		/// <summary>
		/// Rotation for the camera.
		/// </summary>
		/// <param name="yawChange">Change of horizontal rotation.</param>
		/// <param name="pitchChange">Change of vertical rotation.</param>
		public void Rotate(float yawChange, float pitchChange)
		{
			this.Yaw += yawChange;
			this.Pitch += pitchChange;
		}

		public void Move(Vector3 translation)
		{
			this._translation += translation;
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
