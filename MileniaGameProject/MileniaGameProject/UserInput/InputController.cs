using System;
using System.Linq;
using System.Xml.Schema;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MileniaGameProject.Entities;
using MileniaGameProject.SupportFiles;

namespace MileniaGameProject.UserInput
{
    public class InputController
    {
        private KeyboardState _previousKeyboardState;
        private MouseState _previousMouseState;

        public static GameState GameState = GameState.InGame;
        
        private int _previousScrollValue;

        private Character _character;
        private Map _map;
        private NPC talkableNPC;
        private string _textToShow;

        //Determines speed of player
        private int _velocity = 10;

        private double squareOfTwo = Math.Sqrt(2) / 2;
        private int TearFix = 5;

        public InputController(Character character, Map map)
        {
            _character = character;
            _map = map;
        }

        public void ProcessControls(GameTime gameTime, Map map)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            int punish = 0;
            Keys[] keys = keyboardState.GetPressedKeys();

            Keys[] numKeys = new Keys[]
            {
                Keys.D0,
                Keys.D1,
                Keys.D2,
                Keys.D3,
                Keys.D4,
                Keys.D5,
                Keys.D6,
                Keys.D7,
                Keys.D8,
                Keys.D9
            };

            if (GameState == GameState.Talking)
            {

                if (keyboardState.IsKeyDown(Keys.E) && !_previousKeyboardState.IsKeyDown(Keys.E))
                {
                    talkableNPC.IsTalking = false;
                    Milenia.NPCManager.Talking = false;
                    GameState = GameState.InGame;
                }
                else
                {
                    _previousKeyboardState = keyboardState;
                    return;
                }
            }
            else if (keyboardState.IsKeyDown(Keys.E) && !_previousKeyboardState.IsKeyDown(Keys.E) && GameState == GameState.InGame)
            {
                talkableNPC = Milenia.NPCManager.FindTalkableNPC();
                if (talkableNPC != null)
                {
                    GameState = GameState.Talking;
                    talkableNPC.IsTalking = true;
                    _previousKeyboardState = keyboardState;
                    return;
                }
            }

            if (keyboardState.IsKeyDown(Keys.I) && !_previousKeyboardState.IsKeyDown(Keys.I))
            {
                if (GameState != GameState.Inventory)
                {
                    GameState = GameState.Inventory;
                }
                else
                {
                    GameState = GameState.InGame;
                }
            } 
            
            if (keyboardState.IsKeyDown(Keys.K) && !_previousKeyboardState.IsKeyDown(Keys.K))
            {
                if (GameState != GameState.Skilltree)
                {
                    GameState = GameState.Skilltree;
                }
                else
                {
                    GameState = GameState.InGame;
                }
            }

            if (GameState != GameState.Inventory && GameState != GameState.Skilltree)
            {
                bool isWalkUpwardsPressed = (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W));
            bool isWalkDownwardsPressed =
                (_previousKeyboardState.IsKeyDown(Keys.Down) || _previousKeyboardState.IsKeyDown(Keys.S));
            bool isWalkLeftPressed =
                (_previousKeyboardState.IsKeyDown(Keys.Left) || _previousKeyboardState.IsKeyDown(Keys.A));
            bool isWalkRightPressed =
                (_previousKeyboardState.IsKeyDown(Keys.Right) || _previousKeyboardState.IsKeyDown(Keys.D));

            bool isDiagonalUpLeft = isWalkUpwardsPressed && isWalkLeftPressed;
            bool isDiagonalUpRight = isWalkUpwardsPressed && isWalkRightPressed;
            bool isDiagonalDownLeft = isWalkDownwardsPressed && isWalkLeftPressed;
            bool isDiagonalDownRight = isWalkDownwardsPressed && isWalkRightPressed;

            foreach (var number in numKeys)
            {
                if (keyboardState.IsKeyDown(number))
                {
                    if (number != Keys.D0)
                    {
                        UserInterface.curInvSelection = (int) number - ((int) Keys.D0 + 1);
                    }
                    else
                    {
                        UserInterface.curInvSelection = 9;
                    }
                }
            }

            if (mouseState.ScrollWheelValue > _previousScrollValue)
            {
                UserInterface.curInvSelection = ++UserInterface.curInvSelection % 10;
            } else if (mouseState.ScrollWheelValue < _previousScrollValue)
            {
                UserInterface.curInvSelection = UserInterface.curInvSelection != 0 ? --UserInterface.curInvSelection : 9;
            }
            _previousScrollValue = mouseState.ScrollWheelValue;

            if (isWalkLeftPressed)
            {
                _character.State = CharacterState.WalkLeft;
            }
            else if (isWalkRightPressed)
            {
                _character.State = CharacterState.WalkRight;
            }
            else if (isWalkUpwardsPressed)
            {
                _character.State = CharacterState.WalkUp;
            }
            else if (isWalkDownwardsPressed)
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

            if (map.canMoveDown &&
                isWalkDownwardsPressed &&
                map.CameraPosition.Y < (map.MapTexture.Height - Milenia.DefaultHeight) && _character.Position.Y >=
                (Milenia.DefaultHeight - Collision.Height) / 2) // need to subtract character height
            {
                map.CameraPosition.Y += _velocity - punish;
            }
            else if (map.canMoveDown &&
                     isWalkDownwardsPressed &&
                     _character.Position.Y <= (Milenia.DefaultHeight - Collision.Height))
            {
                _character.Position.Y += _velocity - punish;
            }

            if (map.canMoveUp &&
                isWalkUpwardsPressed && _map.CameraPosition.Y > 0 &&
                _character.Position.Y <= Milenia.DefaultHeight / 2 )
            {
                _map.CameraPosition.Y -= _velocity - punish;
            }
            else if (
                map.canMoveUp &&
                isWalkUpwardsPressed && _character.Position.Y >= 0)
            {
                _character.Position.Y -= _velocity - punish;
            }

            if (map.canMoveLeft &&
                isWalkLeftPressed && _map.CameraPosition.X > 0 &&
                _character.Position.X <= (Milenia.DefaultWidth - Collision.Width - TearFix) / 2)
            {
                _map.CameraPosition.X -= _velocity - punish;
            }
            else if (
                map.canMoveLeft &&
                isWalkLeftPressed && _character.Position.X >= 0)
            {
                _character.Position.X -= _velocity - punish;
            }

            if (
                map.canMoveRight &&
                isWalkRightPressed && map.MapTexture.Width - Milenia.DefaultWidth > 0 &&
                _map.CameraPosition.X <= (map.MapTexture.Width - Milenia.DefaultWidth - TearFix) &&
                _character.Position.X >= (Milenia.DefaultWidth - Collision.Width) / 2)
            {
                _map.CameraPosition.X += _velocity - punish;
            }
            else if (
                map.canMoveRight &&
                isWalkRightPressed && _character.Position.X <= (Milenia.DefaultWidth - Collision.Width))
            {
                _character.Position.X += _velocity - punish;
            }
            }
            

            _previousKeyboardState = keyboardState;
            _previousMouseState = mouseState;

            map.canMoveDown = true;
            map.canMoveUp = true;
            map.canMoveLeft = true;
            map.canMoveRight = true;
        }
    }
}