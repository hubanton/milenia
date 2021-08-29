using System;
using System.Xml.Schema;
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
        private int _velocity = 10;

        private double squareOfTwo = Math.Sqrt(2) / 2;

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


            bool isWalkUpwardsPressed = (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W)) && map.canMoveUp;
            bool isWalkDownwardsPressed =
                (_previousKeyboardState.IsKeyDown(Keys.Down) || _previousKeyboardState.IsKeyDown(Keys.S)) && map.canMoveDown;
            bool isWalkLeftPressed =
                (_previousKeyboardState.IsKeyDown(Keys.Left) || _previousKeyboardState.IsKeyDown(Keys.A)) && map.canMoveLeft;
            bool isWalkRightPressed =
                (_previousKeyboardState.IsKeyDown(Keys.Right) || _previousKeyboardState.IsKeyDown(Keys.D)) && map.canMoveRight;

            bool isDiagonalUpLeft = isWalkUpwardsPressed && isWalkLeftPressed;
            bool isDiagonalUpRight = isWalkUpwardsPressed && isWalkRightPressed;
            bool isDiagonalDownLeft = isWalkDownwardsPressed && isWalkLeftPressed;
            bool isDiagonalDownRight = isWalkDownwardsPressed && isWalkRightPressed;

            if (isWalkLeftPressed)
            {
                _character.State = CharacterState.WalkLeft;
            } else if (isWalkRightPressed)
            {
                _character.State = CharacterState.WalkRight;
            } else if (isWalkUpwardsPressed)
            {
                _character.State = CharacterState.WalkUp;
            } else if (isWalkDownwardsPressed)
            {
                _character.State = CharacterState.WalkDown;
            } 
            else
            {
                _character.State = CharacterState.Idle;
            }

            if (isDiagonalUpLeft || isDiagonalUpRight || isDiagonalDownLeft || isDiagonalDownRight)
            {
                punish = (int) (_velocity - _velocity * squareOfTwo);
            }

            Rectangle Collision = _character.CollisionBox;

            if (isWalkDownwardsPressed &&
                map.CameraPosition.Y < ((map.MapTexture.Height - Milenia.DefaultHeight)) && _character.Position.Y >=
                (Milenia.DefaultHeight - Collision.Height) / 2) // need to subtract character height
            {
                map.CameraPosition.Y += _velocity - punish;
            }
            else if (isWalkDownwardsPressed &&
                     _character.Position.Y <= (Milenia.DefaultHeight - Collision.Height))
            {
                _character.Position.Y += _velocity - punish;
            }

            if (isWalkUpwardsPressed && _map.CameraPosition.Y > 0 &&
                _character.Position.Y <= Milenia.DefaultHeight / 2)
            {
                _map.CameraPosition.Y -= _velocity - punish;
            }
            else if (isWalkUpwardsPressed && _character.Position.Y >= 0)
            {
                _character.Position.Y -= _velocity - punish;
            }

            if (isWalkLeftPressed && _map.CameraPosition.X > 0 &&
                _character.Position.X <= (Milenia.DefaultWidth - Collision.Width) / 2)
            {
                _map.CameraPosition.X -= _velocity - punish;
            }
            else if (isWalkLeftPressed && _character.Position.X >= 0)
            {
                _character.Position.X -= _velocity - punish;
            }

            if (isWalkRightPressed && map.MapTexture.Width - Milenia.DefaultWidth > 0 &&
                _map.CameraPosition.X <= (map.MapTexture.Width - Milenia.DefaultWidth) &&
                _character.Position.X >= (Milenia.DefaultWidth - Collision.Width) / 2)
            {
                _map.CameraPosition.X += _velocity - punish;
            }
            else if (isWalkRightPressed && _character.Position.X <= (Milenia.DefaultWidth - Collision.Width))
            {
                _character.Position.X += _velocity - punish;
            }

            _previousKeyboardState = keyboardState;

            map.canMoveDown = true;
            map.canMoveUp = true;
            map.canMoveLeft = true;
            map.canMoveRight = true;
        }
    }
}