using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MileniaGameProject.Entities;

namespace MileniaGameProject.UserInput
{
    public class InputController
    {
        private KeyboardState _previousKeyboardState;

        private Character _character;

        public InputController(Character character)
        {
            _character = character;
        }
        
        public void ProcessControls(GameTime gameTime)
        {

            KeyboardState keyboardState = Keyboard.GetState();

           
            bool isWalkUpwardsPressed = keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W);
            bool isWalkDownwardsPressed = _previousKeyboardState.IsKeyDown(Keys.Down) || _previousKeyboardState.IsKeyDown(Keys.S);
            bool isWalkLeftPressed = _previousKeyboardState.IsKeyDown(Keys.Left) || _previousKeyboardState.IsKeyDown(Keys.A);
            bool isWalkRightPressed = _previousKeyboardState.IsKeyDown(Keys.Right) || _previousKeyboardState.IsKeyDown(Keys.D);

            if (isWalkDownwardsPressed && _character.Position.Y <= 800)
            {
                _character.Position = new Vector2(_character.Position.X, _character.Position.Y + 5);
            }
            if (isWalkUpwardsPressed && _character.Position.Y >= 0)
            {
                _character.Position = new Vector2(_character.Position.X, _character.Position.Y - 5);
            }
            if (isWalkLeftPressed && _character.Position.X >= 0)
            {
                _character.Position = new Vector2(_character.Position.X - 5, _character.Position.Y);
            }
            if (isWalkRightPressed && _character.Position.X <= 1100)
            {
                _character.Position = new Vector2(_character.Position.X + 5, _character.Position.Y);
            } 
            
            _previousKeyboardState = keyboardState;
            

        }
    }
}