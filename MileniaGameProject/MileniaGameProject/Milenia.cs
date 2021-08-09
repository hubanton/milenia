using System;
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

        private Character _character;
        private MapManager _mapManager;

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
            ScaleX = (float)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / DefaultWidth;
            ScaleY = (float)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / DefaultHeight;
            ScaleMatrix = Matrix.CreateScale(ScaleX, ScaleY, 1.0f);
            //  sets to preferred resolution
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            // Causes the Window to fit the screen size
            _graphics.IsFullScreen = true;
            // Tabbing outside of Screen no longer collapses Window
            _graphics.HardwareModeSwitch = false;
            
            //Needed in order to apply previously made changes to window
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            
            _character = new Character(Content.Load<Texture2D>("capybara"), new Vector2(DefaultWidth / 2, DefaultHeight / 2));
            PlayerWidth = (int) (_character.CharTexture.Width);
            PlayerHeight = (int) (_character.CharTexture.Height);
            _mapManager = new MapManager(Content);
            _mapManager.LoadMap("border", _character);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            base.Update(gameTime);
            _mapManager.Update(gameTime);
            
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _spriteBatch.Begin(transformMatrix: ScaleMatrix);
            _mapManager.Draw(gameTime, _spriteBatch);
            _character.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}