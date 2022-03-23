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

        private KeyboardState _keyboardState;
        private MouseState _mouseState;

        public static GameState GameState = GameState.InGame;
        
        // used to see whether to scroll wheel was used or not
        private int _previousScrollValue;
        
        // array for all number keys
        private readonly Keys[] _numKeys = 
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

        // used to determine whether to move camera, character or not at all
        private Character _character;
        private Map _map; 
        
        // used to check if there is a npc the player can talk to and saves that
        private NPC talkableNPC = null;
        private double _lastTimeTalkedTo;

        // determines speed of player
        private int _velocity = 10;

        // saves the square root of two to not calculate it at each frame when moving diagonally
        private readonly double _squareOfTwo = Math.Sqrt(2) / 2;
        // used to prevent black bars showing on the border of the screen
        private int TearFix = 5;

        /// <summary>
        /// constructor method
        /// </summary>
        /// <param name="character">needed to calculate what to move</param>
        /// <param name="map">needed to calculate what to move</param>
        public InputController(Character character, Map map)
        {
            _character = character;
            _map = map;
        }

        /// <summary>
        /// processes controls, moves camera/character and changes game state
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="_map"></param>
        public void ProcessControls(GameTime gameTime)
        {
            _keyboardState = Keyboard.GetState();
            _mouseState = Mouse.GetState();
            
            // used for diagonal movement
            int punish = 0;
            
            Keys[] keys = _keyboardState.GetPressedKeys();
            
            // the player is currently talking to an NPC
            if (GameState == GameState.Talking) 
            {
                // if true finish dialog
                if (_keyboardState.IsKeyDown(Keys.E) && !_previousKeyboardState.IsKeyDown(Keys.E))
                {
                    talkableNPC.IsTalking = false;
                    Milenia.NPCManager.Talking = false;
                    talkableNPC = null;
                    _lastTimeTalkedTo = gameTime.TotalGameTime.TotalSeconds;
                    GameState = GameState.InGame;
                    return;
                }
                
                _previousKeyboardState = _keyboardState;
                return;
            }
            // start dialog with NPC
            if (_keyboardState.IsKeyDown(Keys.E) && !_previousKeyboardState.IsKeyDown(Keys.E) && gameTime.TotalGameTime.TotalSeconds > _lastTimeTalkedTo + 1 && GameState == GameState.InGame)
            {
                talkableNPC = Milenia.NPCManager.FindTalkableNPC();
                if (talkableNPC != null)
                {
                    GameState = GameState.Talking;
                    talkableNPC.IsTalking = true;
                    _previousKeyboardState = _keyboardState;
                    return;
                }
            }

            // open inventory
            if (_keyboardState.IsKeyDown(Keys.I) && !_previousKeyboardState.IsKeyDown(Keys.I))
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
            
            // open skill tree
            if (_keyboardState.IsKeyDown(Keys.K) && !_previousKeyboardState.IsKeyDown(Keys.K))
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

            // move appropriately
            if (GameState != GameState.Inventory && GameState != GameState.Skilltree)
            {
                bool isWalkUpwardsPressed = (_keyboardState.IsKeyDown(Keys.Up) || _keyboardState.IsKeyDown(Keys.W));
                bool isWalkDownwardsPressed =
                (_previousKeyboardState.IsKeyDown(Keys.Down) || _previousKeyboardState.IsKeyDown(Keys.S));
                bool isWalkLeftPressed =
                (_previousKeyboardState.IsKeyDown(Keys.Left) || _previousKeyboardState.IsKeyDown(Keys.A));
                bool isWalkRightPressed =
                (_previousKeyboardState.IsKeyDown(Keys.Right) || _previousKeyboardState.IsKeyDown(Keys.D));

                if (isWalkLeftPressed && isWalkRightPressed)
                {
                    isWalkLeftPressed = false;
                    isWalkRightPressed = false;
                }

                if (isWalkUpwardsPressed && isWalkDownwardsPressed)
                {
                    isWalkUpwardsPressed = false;
                    isWalkDownwardsPressed = false;
                }

                // determines whether player moves diagonally
                bool isDiagonalUpLeft = isWalkUpwardsPressed && isWalkLeftPressed;
                bool isDiagonalUpRight = isWalkUpwardsPressed && isWalkRightPressed;
                bool isDiagonalDownLeft = isWalkDownwardsPressed && isWalkLeftPressed;
                bool isDiagonalDownRight = isWalkDownwardsPressed && isWalkRightPressed;

                // changse item selection in bar based on input
                foreach (var number in _numKeys)
                {
                    if (_keyboardState.IsKeyDown(number))
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

                // changes item selection based on scroll wheel 
                if (_mouseState.ScrollWheelValue > _previousScrollValue)
                {
                    UserInterface.curInvSelection = ++UserInterface.curInvSelection % 10;
                } else if (_mouseState.ScrollWheelValue < _previousScrollValue)
                {
                    UserInterface.curInvSelection = UserInterface.curInvSelection != 0 ? --UserInterface.curInvSelection : 9;
                }
                _previousScrollValue = _mouseState.ScrollWheelValue;

                // updates character state for appropiate animation
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

                // if walking diagonally slow down character
                if (isDiagonalUpLeft || isDiagonalUpRight || isDiagonalDownLeft || isDiagonalDownRight)
                {
                    punish = (int) (_velocity - _velocity * _squareOfTwo);
                }

                // get collisionbox for character measurements
                Rectangle collision = _character.CollisionBox;

                // considers map and camera position to move the character or the background
                // (if player approaches border move character else the camera)
                // also stops the player from walking out of bounds
                if (_map.canMoveDown &&
                    isWalkDownwardsPressed &&
                    _map.CameraPosition.Y < (_map.MapTexture.Height - Milenia.DefaultHeight) && _character.Position.Y >=
                    (Milenia.DefaultHeight - collision.Height) / 2) // need to subtract character height
                {
                    _map.CameraPosition.Y += _velocity - punish;
                }
                else if (_map.canMoveDown &&
                         isWalkDownwardsPressed &&
                         _character.Position.Y <= (Milenia.DefaultHeight - collision.Height))
                {
                    _character.Position.Y += _velocity - punish;
                }

                if (_map.canMoveUp &&
                    isWalkUpwardsPressed && this._map.CameraPosition.Y > 0 &&
                    _character.Position.Y <= Milenia.DefaultHeight / 2 )
                {
                    _map.CameraPosition.Y -= _velocity - punish;
                }
                else if (
                    _map.canMoveUp &&
                    isWalkUpwardsPressed && _character.Position.Y >= 0)
                {
                    _character.Position.Y -= _velocity - punish;
                }

                if (_map.canMoveLeft &&
                    isWalkLeftPressed && _map.CameraPosition.X > 0 &&
                    _character.Position.X <= (Milenia.DefaultWidth - collision.Width - TearFix) / 2)
                {
                    _map.CameraPosition.X -= _velocity - punish;
                }
                else if (
                    _map.canMoveLeft &&
                    isWalkLeftPressed && _character.Position.X >= 0)
                {
                    _character.Position.X -= _velocity - punish;
                }

                if (
                    _map.canMoveRight &&
                    isWalkRightPressed && _map.MapTexture.Width - Milenia.DefaultWidth > 0 &&
                    _map.CameraPosition.X <= (_map.MapTexture.Width - Milenia.DefaultWidth - TearFix) &&
                    _character.Position.X >= (Milenia.DefaultWidth - collision.Width) / 2)
                {
                    _map.CameraPosition.X += _velocity - punish;
                }
                else if (
                    _map.canMoveRight &&
                    isWalkRightPressed && _character.Position.X <= (Milenia.DefaultWidth - collision.Width))
                {
                    _character.Position.X += _velocity - punish;
                }
            }
                

            _previousKeyboardState = _keyboardState;
            _previousMouseState = _mouseState;

            _map.canMoveDown = true;
            _map.canMoveUp = true;
            _map.canMoveLeft = true;
            _map.canMoveRight = true;
        }
    }
}