using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FleetHackers.Cameras
{
	public abstract class AbstractCamera
	{
		/// <summary>
		/// Class variable for graphics device access.
		/// </summary>
		protected GraphicsDevice graphicsDevice;

		/// <summary>
		/// Default constructor for the camera.
		/// </summary>
		/// <param name="graphicsDevice">Pass in the graphics device parameter.</param>
		public AbstractCamera(GraphicsDevice graphicsDevice)
		{
			this.graphicsDevice = graphicsDevice;
			GeneratePerspectiveProjectionMatrix(MathHelper.PiOver4);
		}

		/// <summary>
		/// Maybe we need to make this into an interface too? or just leave it as abstract is ok.
		/// </summary>
		public abstract void Update();

		/// <summary>
		/// Default camera parameters generator. Make this a virtual maybe?
		/// </summary>
		/// <param name="fieldOfView">Specify the parameter for field of view.</param>
		private void GeneratePerspectiveProjectionMatrix(float fieldOfView)
		{
			PresentationParameters presentationParameters = graphicsDevice.PresentationParameters;
			float aspectRatio = (float)presentationParameters.BackBufferWidth / (float)presentationParameters.BackBufferHeight;
			this.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), aspectRatio, 1f, 100000.0f);
		}

		/// <summary>
		/// Gets or sets the camera position and orientation.
		/// </summary>
		public Matrix View
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the camera field of view and depth of frustrum.
		/// </summary>
		public Matrix Projection
		{
			get;
			set;
		}
	}
}
