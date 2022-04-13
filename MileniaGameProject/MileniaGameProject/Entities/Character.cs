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
        public int DrawOrder => 2;

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

        private readonly int _idleX = 0;
        private readonly int _idleY = 0;
        private readonly int _idleWidth = 80;
        private readonly int _idleHeight = 160;
        
        private readonly int _upX = 0;
        private readonly int _upY = 485;
        private readonly int _upWidth = 100;
        private readonly int _upHeight = 150;
        
        private readonly int _downX = 0;
        private readonly int _downY = 650;
        private readonly int _downWidth = 100;
        private readonly int _downHeight = 150;
        
        private readonly int _leftX = 0;
        private readonly int _leftY = 175;
        private readonly int _leftWidth = 120;
        private readonly int _leftHeight = 140;
        
        private readonly int _rightX = 0;
        private readonly int _rightY = 335;
        private readonly int _rightWidth = 120;
        private readonly int _rightHeight = 140;

        // describes how long one sprite should be drawn before the next sprite (in frames)
        public static int RUNNING_ANIMATION_FRAME_DURATION = 6;

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
                

                Rectangle box = new Rectangle((int) Position.X, (int) Position.Y, width, height);
                return box;
            }
        }

        
        
        public Character(Texture2D character, Vector2 pos)
        {
            Position = pos;

            _idleSprite = new Sprite(character, _idleX, _idleY, _idleWidth, _idleHeight);
            _walkUpAnimation = new SpriteAnimation();
            _walkUpAnimation.AddFrame(new Sprite(character, _upX, _upY, _upWidth, _upHeight), 0);
            _walkUpAnimation.AddFrame(new Sprite(character, _upX + _upWidth, _upY, _upWidth, _upHeight), RUNNING_ANIMATION_FRAME_DURATION);
            _walkUpAnimation.AddFrame(new Sprite(character, _upX + (2 * _upWidth), _upY, _upWidth, _upHeight), RUNNING_ANIMATION_FRAME_DURATION * 2);
            _walkUpAnimation.AddFrame(new Sprite(character, _upX + (3 * _upWidth), _upY, _upWidth, _upHeight), RUNNING_ANIMATION_FRAME_DURATION * 3);
            _walkUpAnimation.Play();
            _walkDownAnimation = new SpriteAnimation();
            _walkDownAnimation.AddFrame(new Sprite(character, _downX, _downY, _downWidth, _downHeight), 0);
            _walkDownAnimation.AddFrame(new Sprite(character, _downX + _downWidth, _downY, _downWidth, _downHeight), RUNNING_ANIMATION_FRAME_DURATION);
            _walkDownAnimation.AddFrame(new Sprite(character, _downX + (2 * _downWidth), _downY, _downWidth, _downHeight), RUNNING_ANIMATION_FRAME_DURATION * 2);
            _walkDownAnimation.AddFrame(new Sprite(character, _downX + (3 * _downWidth), _downY, _downWidth, _downHeight), RUNNING_ANIMATION_FRAME_DURATION * 3);
            _walkDownAnimation.Play();
            _walkLeftAnimation = new SpriteAnimation();
            _walkLeftAnimation.AddFrame(new Sprite(character, _leftX, _leftY, _leftWidth, _leftHeight), 0);
            _walkLeftAnimation.AddFrame(new Sprite(character, _leftX + _leftWidth, _leftY, _leftWidth, _leftHeight), RUNNING_ANIMATION_FRAME_DURATION);
            _walkLeftAnimation.AddFrame(new Sprite(character, _leftX + (2 * _leftWidth), _leftY, _leftWidth, _leftHeight), RUNNING_ANIMATION_FRAME_DURATION * 2);
            _walkLeftAnimation.AddFrame(new Sprite(character, _leftX + (3 * _leftWidth), _leftY, _leftWidth, _leftHeight), RUNNING_ANIMATION_FRAME_DURATION * 3);
            _walkLeftAnimation.Play();
            _walkRightAnimation = new SpriteAnimation();
            _walkRightAnimation.AddFrame(new Sprite(character, _rightX, _rightY, _rightWidth, _rightHeight), 0);
            _walkRightAnimation.AddFrame(new Sprite(character, _rightX + _rightWidth, _rightY, _rightWidth, _rightHeight), RUNNING_ANIMATION_FRAME_DURATION);
            _walkRightAnimation.AddFrame(new Sprite(character, _rightX + (2 * _rightWidth), _rightY, _rightWidth, _rightHeight), RUNNING_ANIMATION_FRAME_DURATION * 2);
            _walkRightAnimation.AddFrame(new Sprite(character, _rightX + (3 * _rightWidth), _rightY, _rightWidth, _rightHeight), RUNNING_ANIMATION_FRAME_DURATION * 3);
            _walkRightAnimation.Play();

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


    }
}