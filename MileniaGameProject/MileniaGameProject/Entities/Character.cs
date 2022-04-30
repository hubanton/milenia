using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MileniaGameProject.Graphics;
using MileniaGameProject.SupportFiles;
using MileniaGameProject.UserInput;

namespace MileniaGameProject.Entities
{
    /// <summary>
    /// class for the player character
    /// </summary>
    public class Character : IGameEntity, ICollidable
    {
        public int DrawOrder => 3;

        private readonly Sprite _idleSprite;
        private readonly SpriteAnimation _walkUpAnimation;
        private readonly SpriteAnimation _walkDownAnimation;
        private readonly SpriteAnimation _walkLeftAnimation;
        private readonly SpriteAnimation _walkRightAnimation;

        private int _numLoops;

        public static int Level = 1;
        
        public static int CurHealth = 500;
        public static int MaxHealth = 1000;
        public static int CurMana = 500;
        public static int MaxMana = 1000;
        public static int CurExperience = 500;
        public static int MaxExperience = 1000;

        public static int Strength;
        public static int Endurance;
        public static int Intelligence;
        public static int Precision;
        public static int Charisma;

        private readonly int _walkingFrameCount = 6;
        
        private readonly int _idleX = 0;
        private readonly int _idleY = 0;
        private readonly int _idleWidth = 80;
        private readonly int _idleHeight = 160;
        
        private readonly int _upX = 0;
        private readonly int _upY = 481;
        private readonly int _upWidth = 100;
        private readonly int _upHeight = 160;
        private readonly int _upOffset = 100;
        
        private readonly int _downX = 0;
        private readonly int _downY = 641;
        private readonly int _downWidth = 100;
        private readonly int _downHeight = 160;
        private readonly int _downOffset = 100;
        
        private readonly int _leftX = 0;
        private readonly int _leftY = 161;
        private readonly int _leftWidth = 120;
        private readonly int _leftHeight = 160;
        private readonly int _leftOffset = 125;
        
        private readonly int _rightX = 0;
        private readonly int _rightY = 321;
        private readonly int _rightWidth = 120;
        private readonly int _rightHeight = 160;
        private readonly int _rightOffset = 125;

        // describes how long one sprite should be drawn before the next sprite (in frames)
        public static int RUNNING_ANIMATION_FRAME_DURATION = 8;

        public CharacterState State = CharacterState.Idle;

        public Vector2 Position;

        // cooldown before a new map can be entered
        public int HasJustLoaded;

        public Rectangle CollisionBox
        {
            get
            {
                int width, height;
                switch (State)
                {
                   
                    case CharacterState.WalkUp:
                        width = _walkUpAnimation.CurrentFrame.Sprite.Width; height = _walkUpAnimation.CurrentFrame.Sprite.Height;
                        break;
                    case CharacterState.WalkDown:
                        width = _walkDownAnimation.CurrentFrame.Sprite.Width; height = _walkDownAnimation.CurrentFrame.Sprite.Height;
                        break;
                    case CharacterState.WalkLeft:
                        width = _walkLeftAnimation.CurrentFrame.Sprite.Width; height = _walkLeftAnimation.CurrentFrame.Sprite.Height;
                        break;
                    case CharacterState.WalkRight:
                        width = _walkRightAnimation.CurrentFrame.Sprite.Width; height = _walkRightAnimation.CurrentFrame.Sprite.Height;
                        break;
                    default: 
                        width = _idleSprite.Width; height = _idleSprite.Height;
                        break;
                }

                 int actHeight = (int) (0.5 * height);

                Rectangle box = new Rectangle((int) Position.X, (int) Position.Y + actHeight, width, actHeight);
                return box;
            }
        }

        public Character(Texture2D character, Vector2 pos)
        {
            Position = pos;

            _idleSprite = new Sprite(character, _idleX, _idleY, _idleWidth, _idleHeight);
            
            _walkLeftAnimation = SpriteAnimation.CreateSimpleAnimation(character, new Point(_leftX, _leftY), _leftWidth, _leftHeight, new Point(_leftOffset, 0), 
                _walkingFrameCount, RUNNING_ANIMATION_FRAME_DURATION);

            _walkRightAnimation = SpriteAnimation.CreateSimpleAnimation(character, new Point(_rightX, _rightY), _rightWidth, _rightHeight, new Point(_rightOffset, 0), 
                _walkingFrameCount, RUNNING_ANIMATION_FRAME_DURATION);

            _walkUpAnimation = SpriteAnimation.CreateSimpleAnimation(character, new Point(_upX, _upY), _upWidth, _upHeight, new Point(_upOffset, 0), 
                _walkingFrameCount, RUNNING_ANIMATION_FRAME_DURATION);

            _walkDownAnimation = SpriteAnimation.CreateSimpleAnimation(character, new Point(_downX, _downY), _downWidth, _downHeight, new Point(_downOffset, 0), 
                _walkingFrameCount, RUNNING_ANIMATION_FRAME_DURATION);

        }

        public void Update(GameTime gameTime)
        {
            if (CurHealth < 1000 && _numLoops == 1)
            {
                _numLoops = 0;
                ++CurHealth;
            }
            else
            {
                ++_numLoops;
            }
            switch (State)
            {
                case CharacterState.WalkUp:
                    _walkUpAnimation.Update(gameTime);
                    break;
                case CharacterState.WalkDown:
                    _walkDownAnimation.Update(gameTime);
                    break;
                case CharacterState.WalkLeft:
                    _walkLeftAnimation.Update(gameTime);
                    break;
                case CharacterState.WalkRight:
                    _walkRightAnimation.Update(gameTime);
                    break;
            }

            if (HasJustLoaded != 0)
            {
                --HasJustLoaded;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (InputController.GameState == GameState.InGame)
            {
                switch (State)
                {
                    case CharacterState.WalkUp:
                        _walkUpAnimation.Draw(spriteBatch, Position);
                        break;
                    case CharacterState.WalkDown:
                        _walkDownAnimation.Draw(spriteBatch, Position);
                        break;
                    case CharacterState.WalkLeft:
                        _walkLeftAnimation.Draw(spriteBatch, Position);
                        break;
                    case CharacterState.WalkRight:
                        _walkRightAnimation.Draw(spriteBatch, Position);
                        break;
                    default:
                        _idleSprite.Draw(spriteBatch, Position);
                        break;
                } 
            } else
            {
                    _idleSprite.Draw(spriteBatch, Position);
            } 
            
        }

        public void CheckCollisions()
        {
            // not needed for character
        }
    }
}