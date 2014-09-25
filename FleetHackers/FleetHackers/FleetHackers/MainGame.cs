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
using FleetHackers.Cards;
using System.Diagnostics;
using System.IO;
using FleetHackers.EngineEnums;
using FleetHackers.EngineStructs;
using FleetHackers.Input;
using FleetHackers.UpdateHelpers;
using FleetHackers.FleetHackersServer;

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
		private GraphicsDeviceManager _graphicsDeviceManager;

		/// <summary>
		/// Idk what this is for, maybe we can make stars out of it. 
		/// </summary>
		private SpriteBatch _spriteBatch;
		
		/// <summary>
		/// List of models to be loaded.
		/// TODO: Do we need mor complex models? Animation, dynamic lighting, and such?
		/// </summary>
		private List<BasicModel> _models = new List<BasicModel>();

		/// <summary>
		/// Provides a camera template. Can create concrete camera classes.
		/// </summary>
		private AbstractCamera _camera;

		/// <summary>
		/// This is used to show that the mouse has moved.
		/// </summary>
		private MouseState _lastMouseState;

		/// <summary>
		/// Chooses what kind of camera to use.
		/// </summary>
		private CameraType _cameraType;

		/// <summary>
		/// This will be used to help draw lines in the game.
		/// This is considered a quick and easy way to show overlay stuff.
		/// </summary>
		private LineDrawer _lineDrawer;

		/// <summary>
		/// Stars that make up the background.
		/// </summary>
		private BillboardSystem _stars;

		/// <summary>
		/// The _skybox
		/// </summary>
		private Skybox _skybox;

		/// <summary>
		/// The board plane
		/// </summary>
		private Plane _boardPlane;

		/// <summary>
		/// The movement data reporter
		/// </summary>
		private MovementReport _movementDataReporter;

		/// <summary>
		/// Constructor for this class.
		/// </summary>
		public MainGame()
		{
			_graphicsDeviceManager = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			_graphicsDeviceManager.PreferredBackBufferWidth = WIDTH;
			_graphicsDeviceManager.PreferredBackBufferHeight = HEIGHT;

			// Use this line to switch camera. TODO: switch camera in game.
			_cameraType = CameraType.TargetCamera;
		}

		/// <summary>
		/// Main initialize method for this current game state.
		/// </summary>
		protected override void Initialize()
		{
			_lineDrawer = new LineDrawer(GraphicsDevice);
			IsMouseVisible = true;

			_boardPlane = new Plane(new Vector3(0, 1, 0), 0);

			_movementDataReporter.traveling = false;
			_movementDataReporter.newCoordinates = Vector3.Zero;
			_movementDataReporter.PlayerShipSelected = new Dictionary<string, bool>();

			base.Initialize();
		}

		/// <summary>
		/// Adds models to the game. Add camera to the game. We'll do more with this later.
		/// </summary>
		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			//Load camera.
			if (_cameraType == CameraType.TargetCamera)
			{
				_camera = new TargetCamera(
						(new Vector3(0, 500, 500)) * 3,
						Vector3.Zero, GraphicsDevice);
			}

			if (_cameraType == CameraType.FreeCamera)
			{
				_camera = new FreeCamera(
						(new Vector3(500, 600, 1300)) * 1,
						MathHelper.ToRadians(153),
						MathHelper.ToRadians(5),
						GraphicsDevice);
			}

			//Load models.
			_models.Add(
				new BasicModel(
					Content.Load<Model>("blueship"),
					Vector3.Zero,
					Quaternion.Identity,
					new Vector3(.01f),
					GraphicsDevice));
			_movementDataReporter.PlayerShipSelected.Add("blueship", false);

			//Load sky box.
			_skybox = new Skybox(Content, GraphicsDevice, Content.Load<TextureCube>("Textures\\bluestreak"));


			//Load stars.
			Random r = new Random();
			Vector3[] postions = new Vector3[30000];
			float dispersalRate = 700000;
			float height = -400000;

			for (int i = 0; i < postions.Length; i++)
			{
				postions[i] = new Vector3((float)r.NextDouble() * dispersalRate - dispersalRate / 2, (float)r.NextDouble() * (height) - height / 2, (float)r.NextDouble() * dispersalRate - dispersalRate / 2);
				//postions[i] = new Vector3(10000, 400, 10000);
			}

			_stars = new BillboardSystem(GraphicsDevice, Content, Content.Load<Texture2D>("BillboardTextures\\flare-blue-purple1"), new Vector2(800), postions);

				
			// TEST DESERIALIZATION
			CardCollection cards = CardCollection.Deserialize(File.ReadAllText("Content/Cards/Cards.json"));
			foreach (Card c in cards)
			{
				Debug.WriteLine(c.title);
				Debug.WriteLine(c.rulesText);
			}

			//initialize debug stuff
			DebugDraw._camera = _camera;
			DebugDraw._lineDrawer = _lineDrawer;
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

			BasicModel tempModel = _models[0];
			_movementDataReporter = ShipMovement.MoveShip(_movementDataReporter, gameTime, ref tempModel);
			_models[0] = tempModel;

			if (keystate.IsKeyDown(Keys.Escape))
			{
				this.Exit();
			}

			if (_cameraType == CameraType.TargetCamera)
			{
				_camera.Update();
			}

			if(_cameraType == CameraType.FreeCamera)
			{
				_lastMouseState = ((FreeCamera)_camera).FreeCameraUpdate(gameTime, _camera, _lastMouseState);			
			}

			_movementDataReporter = MouseGestures.CheckMouseClicked(_models, _boardPlane, _movementDataReporter, _camera, GraphicsDevice);
			
	

			base.Update(gameTime);
		}

		/// <summary>
		/// Primary draw method.
		/// </summary>
		/// <param name="gameTime">Handeled by the framework, the passage of time is updated automagically.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			

			_skybox.Draw(_camera.View, _camera.Projection, _camera.Position);

			foreach (BasicModel model in _models)
			{
				if (_camera.BoundingVolumeIsInView(model.BoundingSphere))
				{
					model.Draw(_camera.View, _camera.Projection, _camera.Position);
				}
			}

			_lineDrawer.Begin(_camera.View, _camera.Projection);

			_lineDrawer.DrawHexagonGrid(Vector2.One * - 9930, (new Vector2(80,40)), 200, Color.Red);
			_lineDrawer.DrawLine(_models[0].Position, new Vector3(_models[0].Position.X, 0, _models[0].Position.Z), Color.CornflowerBlue);
			
			ShipMovement.DrawHeading();
			ShipMovement.DrawMarker();

			_lineDrawer.End();

			_stars.Draw(_camera.View, _camera.Projection, _camera.Up, _camera.Right);

			base.Draw(gameTime);
		}
	}
}
