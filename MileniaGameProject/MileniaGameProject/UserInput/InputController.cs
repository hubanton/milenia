using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MileniaGameProject.Entities;

namespace MileniaGameProject.UserInput
{
    public class InputController
    {
        private KeyboardState _previousKeyboardState;

        private Character _character;
        private Map _map;

        public InputController(Character character, Map map)
        {
            _character = character;
            _map = map;
        }
        
        public void ProcessControls(GameTime gameTime)
        {

            KeyboardState keyboardState = Keyboard.GetState();

           
            bool isWalkUpwardsPressed = keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W);
            bool isWalkDownwardsPressed = _previousKeyboardState.IsKeyDown(Keys.Down) || _previousKeyboardState.IsKeyDown(Keys.S);
            bool isWalkLeftPressed = _previousKeyboardState.IsKeyDown(Keys.Left) || _previousKeyboardState.IsKeyDown(Keys.A);
            bool isWalkRightPressed = _previousKeyboardState.IsKeyDown(Keys.Right) || _previousKeyboardState.IsKeyDown(Keys.D);

            if (isWalkDownwardsPressed && _map.Position.Y >= -900 && _character.Position.Y > 325)
            {
                _map.Position.Y -= 5;
            }
            else if (isWalkDownwardsPressed && _character.Position.Y <= 775)
            {
                _character.Position.Y += 5;
            }

            if (isWalkUpwardsPressed && _map.Position.Y <= 0 && _character.Position.Y < 450)
            {
                _map.Position.Y += 5;
            }
            else if (isWalkUpwardsPressed && _character.Position.Y >= 0)
            {
                _character.Position.Y -= 5;
            }

            if (isWalkLeftPressed && _map.Position.X <= 0 && _character.Position.X < 600)
            {
                _map.Position.X += 5;
            }
            else if (isWalkLeftPressed && _character.Position.X >= 0)
            {
                _character.Position.X -= 5;
            }

            if (isWalkRightPressed && _map.Position.X >= -1200 && _character.Position.X > 475)
            {
                _map.Position.X -= 5;
            }
            else if (isWalkRightPressed && _character.Position.X <= 1005)
            {
                _character.Position.X += 5;
            }

            _previousKeyboardState = keyboardState;
            

        }
    }
}