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
using Dhpoware;
using FleetHackersLib.Cards.Enums;
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
		/// Object that will hold the card definitions.
		/// </summary>
		private CardCollection _cards;

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
		private int _cardRulesHeight = 404;

		/// <summary>
		/// The left-most x coordinate of the rules text box on a card.
		/// </summary>
		private int _cardRulesLeftX = 73;

		/// <summary>
		/// The top-most y coordinate of the rules text box on a card.
		/// </summary>
		private int _cardRulesTopY = 582;

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
		/// The width of the type box on a card.
		/// </summary>
		private int _cardTypeWidth = 415;

		/// <summary>
		/// The height of the type box on a card.
		/// </summary>
		private int _cardTypeHeight = 36;

		/// <summary>
		/// The left-most x coordinate of the type box on a card.
		/// </summary>
		private int _cardTypeLeftX = 266;

		/// <summary>
		/// The top-most y coordinate of the type box on a card.
		/// </summary>
		private int _cardTypeTopY = 142;

		/// <summary>
		/// The width of the energy cost area on a card.
		/// </summary>
		private int _cardCostWidth = 105;

		/// <summary>
		/// The height of the energy cost area on a card.
		/// </summary>
		private int _cardCostHeight = 101;

		/// <summary>
		/// The left-most x coordinate of the energy cost area on a card.
		/// </summary>
		private int _cardCostLeftX = 41;

		/// <summary>
		/// The top-most y coordinate of the energy cost area on a card.
		/// </summary>
		private int _cardCostTopY = 49;

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
		/// The font used for the type on a card.
		/// </summary>
		private SpriteFont _typeFont;

		/// <summary>
		/// The font used for statistics on cards.
		/// </summary>
		private SpriteFont _statFont;

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
			_typeFont = Content.Load<SpriteFont>("Fonts\\TypeFont");
			_statFont = Content.Load<SpriteFont>("Fonts\\StatFont");

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
			//_cards = CardCollection.Deserialize(File.ReadAllText("Content/Cards/Cards.json"));
			_cards = CardCollection.Deserialize(File.ReadAllText("Content/Cards/Cards.json"));

			FleetHackers.FleetHackersServer.FleetHackersServiceClient fhClient = new FleetHackers.FleetHackersServer.FleetHackersServiceClient();
			string result = fhClient.GetData(1223); //this test works.
			
			//Appears we're experiencing some schema issues here. Will track down what the issue is.
			//cards = fhClient.GetCardData(new List<Card>()); 

			//cards = fhClient.GetCardData(cards); // This stuff needs fixing. JSON is disabled in the meantime.

			fhClient.Close();

			foreach (Card c in _cards)
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
				// Choosing a random card for testing...
				Random r = new Random();
				//Card c = _cards[r.Next(_cards.Count)];
				Card c = null;
				foreach (Card cx in _cards)
				{
					if (cx.Title == "Engineering Vessel")
					//if (_rulesTextFont.MeasureString(WrapText(_rulesTextFont, cx.RulesText, _cardRulesWidth)).Y > _cardRulesHeight)
					{
						c = cx;
						//Debug.WriteLine("{0}", _rulesTextFont.MeasureString(WrapText(_rulesTextFont, cx.RulesText, _cardRulesWidth)).Y - _cardRulesHeight);
						break;
					}
				}

				string someRandomText = c.RulesText;
				someRandomText = WrapText(_rulesTextFont, someRandomText, _cardRulesWidth);
				Vector2 textSize = _rulesTextFont.MeasureString(someRandomText);

				string titleString = c.Title;
				Vector2 titleSize = _titleFont.MeasureString(titleString);

				string typeString = c.Supertype.ToString();
				string subtypeString = c.Subtype.ToString();
				if (subtypeString != "None")
				{
					typeString = typeString + " - " + c.Subtype.ToString();
				}
				Vector2 typeSize = _typeFont.MeasureString(typeString);

				string costString = (c.EnergyCostType == AmountType.Variable) ? Description.ToDescription(c.EnergyCostVar).Replace("+", string.Empty) : c.EnergyCost.ToString();
				Vector2 costSize = _statFont.MeasureString(costString);

				Rectangle cardRect = new Rectangle(
					0,
					0,
					_cardTexture.Width,
					_cardTexture.Height);

				float titleScale = 1;
				if (titleSize.X > _cardTitleWidth)
				{
					titleScale = _cardTitleWidth / titleSize.X;
				}

				float typeScale = 1;
				if (typeSize.X > _cardTypeWidth)
				{
					typeScale = _cardTypeWidth / typeSize.X;
				}

				float costScale = 1;
				if (costSize.X > _cardCostWidth)
				{
					costScale = _cardCostWidth / costSize.X;
				}

				// render the title text's shadow
				RenderTarget2D titleShadowTarget = new RenderTarget2D(GraphicsDevice, _cardTitleWidth, _cardTitleHeight, false,
					GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
				GraphicsDevice.SetRenderTarget(titleShadowTarget);
				GraphicsDevice.Clear(Color.Transparent);
				_spriteBatch.Begin();
				_spriteBatch.DrawString(_titleFont, titleString,
					new Vector2((int)((_cardTitleWidth - titleSize.X * titleScale) / 2), (int)((_cardTitleHeight - titleSize.Y * titleScale) / 2)),
					Color.White, 0, Vector2.Zero, titleScale, SpriteEffects.None, 0);
				_spriteBatch.End();

				// do blur
				GaussianBlur gaussianBlur = new GaussianBlur(this);
				gaussianBlur.ComputeKernel(4, 2);
				int renderTargetWidth = titleShadowTarget.Width / 2;
				int renderTargetHeight = titleShadowTarget.Height / 2;
				RenderTarget2D rt1 = new RenderTarget2D(GraphicsDevice,
					renderTargetWidth, renderTargetHeight, false,
					GraphicsDevice.PresentationParameters.BackBufferFormat,
					DepthFormat.None);
				RenderTarget2D rt2 = new RenderTarget2D(GraphicsDevice,
					renderTargetWidth, renderTargetHeight, false,
					GraphicsDevice.PresentationParameters.BackBufferFormat,
					DepthFormat.None);
				gaussianBlur.ComputeOffsets(renderTargetWidth, renderTargetHeight);
				Texture2D titleShadowResult = gaussianBlur.PerformGaussianBlur(titleShadowTarget, rt1, rt2, _spriteBatch);

				// render the type text's shadow
				RenderTarget2D typeShadowTarget = new RenderTarget2D(GraphicsDevice, _cardTypeWidth, _cardTypeHeight, false,
					GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
				GraphicsDevice.SetRenderTarget(typeShadowTarget);
				GraphicsDevice.Clear(Color.Transparent);
				_spriteBatch.Begin();
				_spriteBatch.DrawString(_typeFont, typeString,
					new Vector2((int)((_cardTypeWidth - typeSize.X * typeScale) / 2), (int)((_cardTypeHeight - typeSize.Y * typeScale) / 2)),
					Color.White, 0, Vector2.Zero, typeScale, SpriteEffects.None, 0);
				_spriteBatch.End();

				// do blur
				renderTargetWidth = typeShadowTarget.Width / 2;
				renderTargetHeight = typeShadowTarget.Height / 2;
				rt1 = new RenderTarget2D(GraphicsDevice,
					renderTargetWidth, renderTargetHeight, false,
					GraphicsDevice.PresentationParameters.BackBufferFormat,
					DepthFormat.None);
				rt2 = new RenderTarget2D(GraphicsDevice,
					renderTargetWidth, renderTargetHeight, false,
					GraphicsDevice.PresentationParameters.BackBufferFormat,
					DepthFormat.None);
				gaussianBlur.ComputeOffsets(renderTargetWidth, renderTargetHeight);
				Texture2D typeShadowResult = gaussianBlur.PerformGaussianBlur(typeShadowTarget, rt1, rt2, _spriteBatch);

				// render the card
				_renderedCardBuffer = new RenderTarget2D(GraphicsDevice, _cardTexture.Width, _cardTexture.Height, false,
					GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
				GraphicsDevice.SetRenderTarget(_renderedCardBuffer);
				GraphicsDevice.Clear(Color.Transparent);

				_spriteBatch.Begin();
				_spriteBatch.Draw(_cardTexture, cardRect, Color.White);
				_spriteBatch.DrawString(_rulesTextFont, someRandomText,
					new Vector2(_cardRulesLeftX, (int)(_cardRulesTopY + (_cardRulesHeight - textSize.Y) / 2)),
					Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

				_spriteBatch.Draw(titleShadowResult,
					new Rectangle((int)(_cardTitleLeftX + (_cardTitleWidth - titleShadowResult.Width * 2) / 2), (int)(_cardTitleTopY + (_cardTitleHeight - titleShadowResult.Height * 2) / 2), titleShadowResult.Width * 2, titleShadowResult.Height * 2),
					new Rectangle(0, 0, titleShadowResult.Width, titleShadowResult.Height),
					Color.White);
				_spriteBatch.Draw(typeShadowResult,
					new Rectangle((int)(_cardTypeLeftX + (_cardTypeWidth - typeShadowResult.Width * 2) / 2), (int)(_cardTypeTopY + (_cardTypeHeight - typeShadowResult.Height * 2) / 2), typeShadowResult.Width * 2, typeShadowResult.Height * 2),
					new Rectangle(0, 0, typeShadowResult.Width, typeShadowResult.Height),
					Color.White);
				_spriteBatch.DrawString(_titleFont, titleString,
					new Vector2((int)(_cardTitleLeftX + (_cardTitleWidth - titleSize.X * titleScale) / 2), (int)(_cardTitleTopY + (_cardTitleHeight - titleSize.Y * titleScale) / 2)),
					Color.Black, 0, Vector2.Zero, titleScale, SpriteEffects.None, 0);
				_spriteBatch.DrawString(_typeFont, typeString,
					new Vector2((int)(_cardTypeLeftX + (_cardTypeWidth - typeSize.X * typeScale) / 2), (int)(_cardTypeTopY + (_cardTypeHeight - typeSize.Y * typeScale) / 2)),
					Color.Black, 0, Vector2.Zero, typeScale, SpriteEffects.None, 0);
				_spriteBatch.DrawString(_statFont, costString,
					new Vector2((int)(_cardCostLeftX + (_cardCostWidth - costSize.X * costScale) / 2), (int)(_cardCostTopY + (_cardCostHeight - costSize.Y * costScale) / 2)),
					Color.Black, 0, Vector2.Zero, costScale, SpriteEffects.None, 0);

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

			foreach (string w in words)
			{
				string word = w;
				string word2 = null;
				if (word.Contains(Environment.NewLine))
				{
					string[] newLineWords = word.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
					word = newLineWords[0];
					word2 = newLineWords[1];
				}

				Vector2 size = spriteFont.MeasureString(word);

				if (lineWidth + size.X < maxLineWidth)
				{
					stringBuilder.Append(word + " ");
					lineWidth += size.X + spaceWidth;
				}
				else
				{
					stringBuilder.Append(Environment.NewLine + word + " ");
					lineWidth = size.X + spaceWidth;
				}

				if (word2 != null)
				{
					size = spriteFont.MeasureString(word2);
					stringBuilder.Append(Environment.NewLine + word2 + " ");
					lineWidth = size.X + spaceWidth;
				}
			}

			return stringBuilder.ToString();
		}
	}
}
