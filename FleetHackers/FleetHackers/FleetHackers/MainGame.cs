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
using FleetHackersLib.Cards;
using System.Diagnostics;
using System.IO;
using FleetHackers.EngineEnums;
using FleetHackers.EngineStructs;
using FleetHackers.Input;
using FleetHackers.UpdateHelpers;
using System.Text;
//using FleetHackers.FleetHackersServer;

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
		/// The card texture.
		/// </summary>
		private Texture2D _cardTexture;

		/// <summary>
		/// The basic font.
		/// </summary>
		private SpriteFont _basicFont;

		/// <summary>
		/// The width of the rules text box on a card.
		/// </summary>
		private int _cardRulesWidth = 608;

		/// <summary>
		/// The height of the rules text box on a card.
		/// </summary>
		private int _cardRulesHeight = 302;

		/// <summary>
		/// The left-most x coordinate of the rules text box on a card.
		/// </summary>
		private int _cardRulesLeftX = 73;

		/// <summary>
		/// The top-most y coordinate of the rules text box on a card.
		/// </summary>
		private int _cardRulesTopY = 684;

		/// <summary>
		/// The width of the title box on a card.
		/// </summary>
		private int _cardTitleWidth = 459;

		/// <summary>
		/// The height of the title box on a card.
		/// </summary>
		private int _cardTitleHeight = 57;

		/// <summary>
		/// The left-most x coordinate of the title box on a card.
		/// </summary>
		private int _cardTitleLeftX = 216;

		/// <summary>
		/// The top-most y coordinate of the title box on a card.
		/// </summary>
		private int _cardTitleTopY = 65;

		/// <summary>
		/// The font used for normal text in the rules text box.
		/// </summary>
		private SpriteFont _rulesTextFont;

		/// <summary>
		/// The font used for italic text in the rules text box.
		/// </summary>
		private SpriteFont _rulesTextItalicFont;

		/// <summary>
		/// The font used for the title on a card.
		/// </summary>
		private SpriteFont _titleFont;

		/// <summary>
		/// A buffer to hold a pre-rendered card so we don't have to re-draw it each time.
		/// </summary>
		private RenderTarget2D _renderedCardBuffer;

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

			// Load Card texture.
			_cardTexture = Content.Load<Texture2D>("Cards\\Images\\cardlayout-2");

			// Load some fonts.
			_basicFont = Content.Load<SpriteFont>("Fonts\\SpriteFont1");
			_rulesTextFont = Content.Load<SpriteFont>("Fonts\\RulesTextFont");
			_rulesTextItalicFont = Content.Load<SpriteFont>("Fonts\\RulesTextItalicFont");
			_titleFont = Content.Load<SpriteFont>("Fonts\\TitleFont");

			// Load models.
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
			//CardCollection cards = CardCollection.Deserialize(File.ReadAllText("Content/Cards/Cards.json"));
			CardCollection cards = CardCollection.Deserialize(File.ReadAllText("Content/Cards/Cards.json"));

			FleetHackers.FleetHackersServer.FleetHackersServiceClient fhClient = new FleetHackers.FleetHackersServer.FleetHackersServiceClient();
			string result = fhClient.GetData(1223); //this test works.
			
			//Appears we're experiencing some schema issues here. Will track down what the issue is.
			//cards = fhClient.GetCardData(new List<Card>()); 

			//cards = fhClient.GetCardData(cards); // This stuff needs fixing. JSON is disabled in the meantime.

			fhClient.Close();

			foreach (Card c in cards)
			{
				Debug.WriteLine(c.Title);
				Debug.WriteLine(c.RulesText);
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

			// if rendered card buffer is null, initialize it here
			if (_renderedCardBuffer == null)
			{
				_renderedCardBuffer = new RenderTarget2D(GraphicsDevice, _cardTexture.Width, _cardTexture.Height, false,
					GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
				GraphicsDevice.SetRenderTarget(_renderedCardBuffer);
				GraphicsDevice.Clear(Color.Transparent);

				// Note: this is arbitrary rules text for testing...
				string someRandomText = "Other Megadrone ships gain 1 range.";
				someRandomText = WrapText(_rulesTextFont, someRandomText, _cardRulesWidth);
				Vector2 textSize = _rulesTextFont.MeasureString(someRandomText);

				string titleString = "Long-Range Megadrone";
				Vector2 titleSize = _titleFont.MeasureString(titleString);

				Rectangle cardRect = new Rectangle(
					0,
					0,
					_cardTexture.Width,
					_cardTexture.Height);

				_spriteBatch.Begin();
				_spriteBatch.Draw(_cardTexture, cardRect, Color.White);
				_spriteBatch.DrawString(_rulesTextFont, someRandomText,
					new Vector2(_cardRulesLeftX, (int)(_cardRulesTopY + (_cardRulesHeight - textSize.Y) / 2)),
					Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

				float textScale = 0;
				if (titleSize.X > _cardTitleWidth)
				{
					textScale = _cardTitleWidth / titleSize.X;
				}
				_spriteBatch.DrawString(_titleFont, titleString,
					new Vector2((int)(_cardTitleLeftX + (_cardTitleWidth - titleSize.X * textScale) / 2), (int)(_cardTitleTopY + (_cardTitleHeight - titleSize.Y * textScale) / 2)),
					Color.Black, 0, Vector2.Zero, textScale, SpriteEffects.None, 0);

				_spriteBatch.End();

				GraphicsDevice.SetRenderTarget(null);
			}

			// Draw cards *this should always appear at the end so its on top
			_spriteBatch.Begin();			
			float scale = .4f;

			// Note: this is arbitrary rules text for testing...
			Rectangle retval = new Rectangle(
				0 + 10,
				GraphicsDevice.Viewport.Height - (int)(_cardTexture.Height * scale) - 10,
				(int)(_cardTexture.Width * scale),
				(int)(_cardTexture.Height * scale));

			_spriteBatch.Draw(_renderedCardBuffer, retval, Color.White);

			_spriteBatch.End();

			base.Draw(gameTime);
		}

		private string WrapText(SpriteFont spriteFont, string text, float maxLineWidth)
		{
			string[] words = text.Split(' ');
			StringBuilder stringBuilder = new StringBuilder();
			float lineWidth = 0f;
			float spaceWidth = spriteFont.MeasureString(" ").X;

			foreach (string word in words)
			{
				Vector2 size = spriteFont.MeasureString(word);

				if (lineWidth + size.X < maxLineWidth)
				{
					stringBuilder.Append(word + " ");
					lineWidth += size.X + spaceWidth;
				}
				else
				{
					stringBuilder.Append("\n" + word + " ");
					lineWidth = size.X + spaceWidth;
				}
			}

			return stringBuilder.ToString();
		}
	}
}
