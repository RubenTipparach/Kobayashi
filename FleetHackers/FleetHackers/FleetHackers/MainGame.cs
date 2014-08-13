namespace FleetHackers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using FleetHackers.Cameras;
	using FleetHackers.Models;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Audio;
	using Microsoft.Xna.Framework.Content;
	using Microsoft.Xna.Framework.GamerServices;
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework.Input;
	using Microsoft.Xna.Framework.Media;

	/// <summary>
	/// This class represents the main game state.
	/// </summary>
	public class MainGame : Microsoft.Xna.Framework.Game
	{
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
		/// Screen width. TODO: Make this configurable.
		/// </summary>
		private const int WIDTH = 1600;

		/// <summary>
		/// Screen Height. TODO: Make this configurable also.
		/// </summary>
		private const int HEIGHT = 900;

		/// <summary>
		/// Constructor for this class.
		/// </summary>
		public MainGame()
		{
			graphicsDeviceManager = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			graphicsDeviceManager.PreferredBackBufferWidth = WIDTH;
			graphicsDeviceManager.PreferredBackBufferHeight = HEIGHT;
		}

		/// <summary>
		/// Main initialize method for this current game state.
		/// </summary>
		protected override void Initialize()
		{
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
					Vector3.Zero,
					Vector3.Zero,
					new Vector3(.4f),
					GraphicsDevice));

			camera = new TargetCamera(
					(new Vector3(500, 600, 1300)) * 10,
					Vector3.Zero, GraphicsDevice);
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

			camera.Update();

			base.Update(gameTime);
		}

		/// <summary>
		/// Primary draw method.
		/// </summary>
		/// <param name="gameTime">Handeled by the framework, the passage of time is updated automagically.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.MidnightBlue);
			models[0].Draw(camera.View, camera.Projection);

			base.Draw(gameTime);
		}
	}
}
