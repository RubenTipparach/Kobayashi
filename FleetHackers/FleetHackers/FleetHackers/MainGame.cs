using System;
using System.Collections.Generic;
using System.Linq;
using FleetHackers.Cameras;
using FleetHackers.DrawingHelpers;
using FleetHackers.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FleetHackers
{

	/// <summary>
	/// This class represents the main game state.
	/// </summary>
	public class MainGame : Microsoft.Xna.Framework.Game
	{
		/// <summary>
		/// Screen width. TODO: Make this configurable.
		/// </summary>
		private const int WIDTH = 1280;

		/// <summary>
		/// Screen Height. TODO: Make this configurable also.
		/// </summary>
		private const int HEIGHT = 720;

		/// <summary>
		/// Graphics device object.
		/// </summary>
		private GraphicsDeviceManager graphicsDeviceManager;

		/// <summary>
		/// Idk what this is for, maybe we can make stars out of it. 
		/// </summary>
		private SpriteBatch spriteBatch;
		
		/// <summary>
		/// List of models to be loaded.
		/// TODO: Do we need mor complex models? Animation, dynamic lighting, and such?
		/// </summary>
		private List<BasicModel> models = new List<BasicModel>();

		/// <summary>
		/// Provides a camera template. Can create concrete camera classes.
		/// </summary>
		private AbstractCamera camera;

		/// <summary>
		/// This is used to show that the mouse has moved.
		/// </summary>
		private MouseState lastMouseState;

		/// <summary>
		/// Chooses what kind of camera to use.
		/// </summary>
		private CameraType cameraType;

		/// <summary>
		/// This will be used to help draw lines in the game.
		/// This is considered a quick and easy way to show overlay stuff.
		/// </summary>
		private LineDrawer lineDrawer;

		/// <summary>
		/// Constructor for this class.
		/// </summary>
		public MainGame()
		{
			graphicsDeviceManager = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			graphicsDeviceManager.PreferredBackBufferWidth = WIDTH;
			graphicsDeviceManager.PreferredBackBufferHeight = HEIGHT;

			cameraType = CameraType.TargetCamera;
		}

		/// <summary>
		/// Main initialize method for this current game state.
		/// </summary>
		protected override void Initialize()
		{
			lineDrawer = new LineDrawer(GraphicsDevice);

			base.Initialize();
		}

		/// <summary>
		/// Adds models to the game. Add camera to the game. We'll do more with this later.
		/// </summary>
		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			models.Add(
				new BasicModel(
					Content.Load<Model>("blueship"),
					Vector3.UnitY * 2500,
					Vector3.Zero,
					new Vector3(.4f),
					GraphicsDevice));

			if (cameraType == CameraType.TargetCamera)
			{
				camera = new TargetCamera(
						(new Vector3(0, 1000, 1000)) * 30,
						Vector3.Zero, GraphicsDevice);
			}

			if (cameraType == CameraType.FreeCamera)
			{
				camera = new FreeCamera(
						(new Vector3(500, 600, 1300)) * 10,
						MathHelper.ToRadians(153),
						MathHelper.ToRadians(5),
						GraphicsDevice);
			}
		}

		/// <summary>
		/// Deconstructor, useful for when we are transitioning game states, and need to free up some much useful memory.
		/// </summary>
		protected override void UnloadContent()
		{
		}

		/// <summary>
		/// Primary update method.
		/// </summary>
		/// <param name="gameTime">This game time variable is updated automagically.</param>
		protected override void Update(GameTime gameTime)
		{
			KeyboardState keystate = Keyboard.GetState();

			if (keystate.IsKeyDown(Keys.Escape))
			{
				this.Exit();
			}

			if (cameraType == CameraType.TargetCamera)
			{
				TargetCameraUpdate();
			}

			if(cameraType == CameraType.FreeCamera)
			{
				FreeCameraUpdate(gameTime);			
			}

			base.Update(gameTime);
		}

		/// <summary>
		/// Primary draw method.
		/// </summary>
		/// <param name="gameTime">Handeled by the framework, the passage of time is updated automagically.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			foreach (BasicModel model in models)
			{
				if (camera.BoundingVolumeIsInView(model.BoundingSphere))
				{
					model.Draw(camera.View, camera.Projection);
				}
			}

			lineDrawer.Begin(camera.View, camera.Projection);
			lineDrawer.DrawHexagonGrid(Vector2.One*(-40000), Vector2.One*40, 2000, Color.Red);
			lineDrawer.DrawLine(models[0].Position, new Vector3(models[0].Position.X, 0, models[0].Position.Z), Color.CornflowerBlue);
			lineDrawer.End();

			base.Draw(gameTime);
		}

		/// <summary>
		/// Controls the behavior of the Target camera.
		/// </summary>
		private void TargetCameraUpdate()
		{
			camera.Update();
		}

		/// <summary>
		/// Controls the behavoir of the Free camera.
		/// TODO: Move input logic to Free Camera class.
		/// </summary>
		private void FreeCameraUpdate(GameTime gameTime)
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
		/// Type of camera to use.
		/// </summary>
		public enum CameraType
		{
			TargetCamera,
			FreeCamera
		}
	}
}
