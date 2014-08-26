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
		protected GraphicsDevice _graphicsDevice;

		/// <summary>
		/// Hide this view variable.
		/// </summary>
		protected Matrix _view;

		/// <summary>
		/// Hide this projection variable.
		/// </summary>
		protected Matrix _projection;

		/// <summary>
		/// Default constructor for the camera.
		/// </summary>
		/// <param name="graphicsDevice">Pass in the graphics device parameter.</param>
		public AbstractCamera(GraphicsDevice graphicsDevice)
		{
			_graphicsDevice = graphicsDevice;
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
			PresentationParameters presentationParameters = _graphicsDevice.PresentationParameters;
			float aspectRatio = (float)presentationParameters.BackBufferWidth / (float)presentationParameters.BackBufferHeight;
			this.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), aspectRatio, 1f, 1000000.0f);
		}

		/// <summary>
		/// Generate bounding frustrum to use for culling objects.
		/// </summary>
		private void GenerateFrustrum()
		{
			Matrix viewProjection = View * Projection;
			BoundingFrustrum = new BoundingFrustum(viewProjection);
		}

		public bool BoundingVolumeIsInView(BoundingSphere sphere)
		{
			return (BoundingFrustrum.Contains(sphere) != ContainmentType.Disjoint);
		}

		public bool BoundingVolumeIsInView(BoundingBox box)
		{
			return (BoundingFrustrum.Contains(box) != ContainmentType.Disjoint);
		}

		/// <summary>
		/// A frustrum used for culling objects not in the view.
		/// </summary>
		public BoundingFrustum BoundingFrustrum
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the camera position and orientation.
		/// </summary>
		public Matrix View
		{
			get
			{
				return _view;
			}
			protected set
			{
				_view = value;
				GenerateFrustrum();
			}
		}

		/// <summary>
		/// Gets or sets the camera field of view and depth of frustrum.
		/// </summary>
		public Matrix Projection
		{
			get
			{
				return _projection;
			}
			protected set
			{
				_projection = value;
				GenerateFrustrum();
			}
		}

		/// <summary>
		/// Camera up vector.
		/// </summary>
		public Vector3 Up
		{
			get;
			protected set;
		}

		/// <summary>
		/// Camera right vector.
		/// </summary>
		public Vector3 Right
		{
			get;
			protected set;
		}

		/// <summary>
		/// Gets or sets the current position.
		/// </summary>
		public Vector3 Position
		{
			get;
			set;
		}
	}
}
