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
        private Map _mapLarge;
        
        private InputController _inputController;

        public static int ScreenWidth;
        public static int ScreenHeight;
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
            ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            ScaleX = (float)ScreenWidth / 1600;
            ScaleY = (float)ScreenHeight / 900;
            ScaleMatrix = Matrix.CreateScale(ScaleX, ScaleY, 1.0f);
            //  sets to preferred resolution
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
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
            
            
            _character = new Character(Content.Load<Texture2D>("capybara"), new Vector2(ScreenWidth / 2, ScreenHeight / 2));
            PlayerWidth = (int) (_character.CharTexture.Width);
            PlayerHeight = (int) (_character.CharTexture.Height);
            _mapLarge = new Map(Content.Load<Texture2D>("border"), _character.Position);
            _inputController = new InputController(_character, _mapLarge);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            base.Update(gameTime);
            
            //Activates Input Listener for KeyboardControls
            _inputController.ProcessControls(gameTime, _mapLarge);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _spriteBatch.Begin();
            _mapLarge.Draw(gameTime, _spriteBatch);
            _character.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}