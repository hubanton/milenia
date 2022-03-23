using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MileniaGameProject.Entities;
using MileniaGameProject.UserInput;

namespace MileniaGameProject
{
    public class Milenia : Game
    {
        // needed to fit game to screen size
        private GraphicsDeviceManager _graphics;
        // used to draw sprites
        private SpriteBatch _spriteBatch;
        
        // collects all managers and calls their update/draw functions at each frame
        private EntityManager _entityManager;

        // all Manager objects that control the update/draw functions of all objects of their domain
        private Character _character;
        private UserInterface _userInterface;
        private ToggleInterface _toggleInterface;
        public static MapManager MapManager;
        public static ForegroundObstacleManager ForegroundObstacleManager;
        public static BackgroundObstacleManager BackgroundObstacleManager;
        public static BuildingManager BuildingManager;
        public static NPCManager NPCManager;

        //  parameters needed for draw functions
        public static int DefaultWidth = 1600;
        public static int DefaultHeight = 900;
        private static Matrix _scaleMatrix;

        /// <summary>
        /// Constructor method
        /// </summary>
        public Milenia()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        
        /// <summary>
        /// currently only initializes settings for screen
        /// </summary>
        protected override void Initialize()
        {
            // Fetches the current resolution of the users screen and sets scale to fit sprites
            float scaleX = (float) GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / DefaultWidth;
            float scaleY = (float) GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / DefaultHeight;
            _scaleMatrix = Matrix.CreateScale(scaleX, scaleY, 1.0f);
            //  sets to preferred resolution
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            // Causes the Window to fit the screen size
            _graphics.IsFullScreen = (2 < 3);
            // Tabbing outside of Screen no longer collapses Window
            _graphics.HardwareModeSwitch = false;

            //Needed in order to apply previously made changes to window
            _graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// creates all Manager objects and loads inital sprites to be shown
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // loads manager objects and assets for the game
            _character = new Character(Content.Load<Texture2D>("char"),
                new Vector2(DefaultWidth / 2, DefaultHeight / 2));

            _userInterface =
                new UserInterface(Content.Load<Texture2D>("inventoryBar"), Content.Load<Texture2D>("invSelect"), Content.Load<Texture2D>("statBar"),
                    Content.Load<Texture2D>("hpBar"), Content.Load<Texture2D>("manaBar"), Content.Load<Texture2D>("expBar"));
            _toggleInterface = new ToggleInterface(Content.Load<Texture2D>("inventoryWhole"),
                Content.Load<Texture2D>("Skilltree"), Content.Load<Texture2D>("DarkBox"));

            
            
            NPCManager = new NPCManager(Content, Content.Load<SpriteFont>("NPCfont"));
            BuildingManager = new BuildingManager(Content);
            ForegroundObstacleManager = new ForegroundObstacleManager(Content);
            BackgroundObstacleManager = new BackgroundObstacleManager(Content);
            
            MapManager = new MapManager(Content);
            List<(Rectangle, String)> entryPoints = new List<(Rectangle, string)>();
            entryPoints.Add((new Rectangle(0, 580, 1, 100), "TownMap"));
            entryPoints.Add((new Rectangle(2399, 800, 1, 100), "TowerMap"));
            MapManager.LoadMap("PlayerBaseProto", _character, entryPoints, Vector2.Zero);
            
            _entityManager = new EntityManager();
            _entityManager.AddEntity(_character);
            _entityManager.AddEntity(MapManager);
            _entityManager.AddEntity(BuildingManager);
            _entityManager.AddEntity(NPCManager);
            _entityManager.AddEntity(ForegroundObstacleManager);
            _entityManager.AddEntity(BackgroundObstacleManager);
            _entityManager.AddEntity(_userInterface);
            _entityManager.AddEntity(_toggleInterface);
        }

        /// <summary>
        /// is called at each frame and updates the game state (processing player input and such...)
        /// by calling entityManagers update function that calls every other nevessary update function
        /// </summary>
        /// <param name="gameTime">time since the application started</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _entityManager.Update(gameTime);

            base.Update(gameTime);
        }
        
        /// <summary>
        /// is called at each frame and draws the current gamestate
        /// by calling entityManagers draw function that calls every other nevessary draw function
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(transformMatrix: _scaleMatrix);
            _entityManager.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}