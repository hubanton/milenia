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
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private EntityManager _entityManager;

        private Character _character;
        private UserInterface _userInterface;
        public static MapManager MapManager;
        public static ForegroundObstacleManager ForegroundObstacleManager;
        public static BackgroundObstacleManager BackgroundObstacleManager;
        public static BuildingManager BuildingManager;

        public static int DefaultWidth = 1600;
        public static int DefaultHeight = 900;
        public static float ScaleX;
        public static float ScaleY;
        public static Matrix ScaleMatrix;
        public static int PlayerWidth;
        public static int PlayerHeight;


        public Milenia()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Fetches the current resolution of the users screen and sets scale to fit sprites
            ScaleX = (float) GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / DefaultWidth;
            ScaleY = (float) GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / DefaultHeight;
            ScaleMatrix = Matrix.CreateScale(ScaleX, ScaleY, 1.0f);
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

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            BuildingManager = new BuildingManager(Content);
            ForegroundObstacleManager = new ForegroundObstacleManager(Content);
            BackgroundObstacleManager = new BackgroundObstacleManager(Content);

            _character = new Character(Content.Load<Texture2D>("char"),
                new Vector2(DefaultWidth / 2, DefaultHeight / 2));

            _userInterface =
                new UserInterface(Content.Load<Texture2D>("inventoryBar"), Content.Load<Texture2D>("invSelect"), Content.Load<Texture2D>("statBar"),
                    Content.Load<Texture2D>("hpBar"), Content.Load<Texture2D>("manaBar"), Content.Load<Texture2D>("expBar"));

            MapManager = new MapManager(Content);
            List<(Rectangle, String)> entryPoints = new List<(Rectangle, string)>();
            entryPoints.Add((new Rectangle(0, 580, 1, 100), "TownMap"));
            entryPoints.Add((new Rectangle(2399, 800, 1, 100), "TowerMap"));
            MapManager.LoadMap("PlayerBaseProto", _character, entryPoints, Vector2.Zero);

            _entityManager = new EntityManager();
            _entityManager.AddEntity(_character);
            _entityManager.AddEntity(MapManager);
            _entityManager.AddEntity(BuildingManager);
            _entityManager.AddEntity(ForegroundObstacleManager);
            _entityManager.AddEntity(BackgroundObstacleManager);
            _entityManager.AddEntity(_userInterface);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _entityManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: ScaleMatrix);
            _entityManager.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}