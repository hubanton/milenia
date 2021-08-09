using System;
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

        //Determines speed of player
        private int _velocity = 8;
        
        public InputController(Character character, Map map)
        {
            _character = character;
            _map = map;
        }

        public void ProcessControls(GameTime gameTime, Map map)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            int punish = 0;
            Keys[] keys = keyboardState.GetPressedKeys();


            bool isWalkUpwardsPressed = keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W);
            bool isWalkDownwardsPressed =
                _previousKeyboardState.IsKeyDown(Keys.Down) || _previousKeyboardState.IsKeyDown(Keys.S);
            bool isWalkLeftPressed =
                _previousKeyboardState.IsKeyDown(Keys.Left) || _previousKeyboardState.IsKeyDown(Keys.A);
            bool isWalkRightPressed =
                _previousKeyboardState.IsKeyDown(Keys.Right) || _previousKeyboardState.IsKeyDown(Keys.D);

            bool isDiagonalUpLeft = isWalkUpwardsPressed && isWalkLeftPressed;
            bool isDiagonalUpRight = isWalkUpwardsPressed && isWalkRightPressed;
            bool isDiagonalDownLeft = isWalkDownwardsPressed && isWalkLeftPressed;
            bool isDiagonalDownRight = isWalkDownwardsPressed && isWalkRightPressed;

            if (isDiagonalUpLeft || isDiagonalUpRight || isDiagonalDownLeft || isDiagonalDownRight)
            {
                punish = (int) (_velocity - _velocity * (Math.Sqrt(2) / 2));
            }

            if (isWalkDownwardsPressed &&
                map.Position.Y <= ((map.MapTexture.Height - Milenia.DefaultHeight)) && _character.Position.Y >= (Milenia.DefaultHeight - _character.CharTexture.Height) / 2)
            {
                map.Position.Y += _velocity - punish;
            }
            else if (isWalkDownwardsPressed &&
                     _character.Position.Y <= (Milenia.DefaultHeight - Milenia.PlayerHeight))
            {
                _character.Position.Y += _velocity - punish;
            }

            if (isWalkUpwardsPressed && _map.Position.Y >= 0 &&
                _character.Position.Y <= Milenia.DefaultHeight / 2)
            {
                _map.Position.Y -= _velocity - punish;
            }
            else if (isWalkUpwardsPressed && _character.Position.Y >= 0)
            {
                _character.Position.Y -= _velocity - punish;
            }

            if (isWalkLeftPressed && _map.Position.X >= 0 && _character.Position.X <= (Milenia.DefaultWidth - Milenia.PlayerWidth) / 2)
            {
                _map.Position.X -= _velocity - punish;
            }
            else if (isWalkLeftPressed && _character.Position.X >= 0)
            {
                _character.Position.X -= _velocity - punish;
            }

            if (isWalkRightPressed && _map.Position.X <= (map.MapTexture.Width - Milenia.DefaultWidth) &&
                _character.Position.X >= (Milenia.DefaultWidth - Milenia.PlayerWidth) / 2)
            {
                _map.Position.X += _velocity - punish;
            }
            else if (isWalkRightPressed && _character.Position.X <= (Milenia.DefaultWidth - Milenia.PlayerWidth))
            {
                _character.Position.X += _velocity - punish;
            }

            _previousKeyboardState = keyboardState;
        }
    }
}