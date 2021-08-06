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
        private Map _mapSmall, _mapLarge;
        
        private InputController _inputController;
        
        public Milenia()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            
            
            _mapSmall = new Map(Content.Load<Texture2D>("pallettown"));
            _mapLarge = new Map(Content.Load<Texture2D>("Oversizedbackground"));
            _character = new Character(Content.Load<Texture2D>("capybara"));
            _inputController = new InputController(_character);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            base.Update(gameTime);
            
            //Activates Input Listener for KeyboardControlls
            _inputController.ProcessControls(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _mapLarge.Draw(gameTime, _spriteBatch );
            //_mapSmall.Draw(gameTime, _spriteBatch);
            _character.Draw(gameTime, _spriteBatch);


            base.Draw(gameTime);
        }
    }
}