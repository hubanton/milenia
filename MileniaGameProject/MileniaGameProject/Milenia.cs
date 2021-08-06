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
            // Fetches the current resolution of the users screen and sets it to preferred resolution
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
            
            
            _character = new Character(Content.Load<Texture2D>("capybara"));
            _character.Position = new Vector2(600, 450);
            _mapLarge = new Map(Content.Load<Texture2D>("Oversizedbackground"));
            _inputController = new InputController(_character, _mapLarge);
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
            
            _mapLarge.Draw(gameTime, _spriteBatch);
            _character.Draw(gameTime, _spriteBatch);


            base.Draw(gameTime);
        }
    }
}