using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MileniaGameProject.Content.Graphics;

namespace MileniaGameProject.Entities
{
    public class Character : IGameEntity, ICollidable
    {
        public int DrawOrder => 3;

        public Sprite IdleSprite;
        public SpriteAnimation WalkUpSprite;
        public SpriteAnimation WalkDownSprite;
        public SpriteAnimation WalkLeftSprite;
        public SpriteAnimation WalkRightSprite;

        private int _idleX = 0;
        private int _idleY = 0;
        private int _idleWidth = 80;
        private int _idleHeight = 160;
        
        private int _upX = 0;
        private int _upY = 485;
        private int _upWidth = 100;
        private int _upHeight = 150;
        
        private int _downX = 0;
        private int _downY = 650;
        private int _downWidth = 100;
        private int _downHeight = 150;
        
        private int _leftX = 0;
        private int _leftY = 175;
        private int _leftWidth = 120;
        private int _leftHeight = 140;
        
        private int _rightX = 0;
        private int _rightY = 335;
        private int _rightWidth = 120;
        private int _rightHeight = 140;

        public static int RUNNING_ANIMATION_LENGTH = 6;

        public CharacterState State = CharacterState.Idle;

        public Vector2 Position;

        public Rectangle CollisionBox
        {
            get
            {
                int width, height;
                width = IdleSprite.Width;
                height = IdleSprite.Height;

                Rectangle box = new Rectangle((int) Position.X, (int) Position.Y, width, height);
                return box;
            }
        }

        public Character(Texture2D character, Vector2 pos)
        {
            Position = pos;

            IdleSprite = new Sprite(character, _idleX, _idleY, _idleWidth, _idleHeight);
            WalkUpSprite = new SpriteAnimation();
            WalkUpSprite.AddFrame(new Sprite(character, _upX, _upY, _upWidth, _upHeight), 0);
            WalkUpSprite.AddFrame(new Sprite(character, _upX + _upWidth, _upY, _upWidth, _upHeight), RUNNING_ANIMATION_LENGTH);
            WalkUpSprite.AddFrame(new Sprite(character, _upX + (2 * _upWidth), _upY, _upWidth, _upHeight), RUNNING_ANIMATION_LENGTH * 2);
            WalkUpSprite.AddFrame(new Sprite(character, _upX + (3 * _upWidth), _upY, _upWidth, _upHeight), RUNNING_ANIMATION_LENGTH * 3);
            WalkUpSprite.Play();
            WalkDownSprite = new SpriteAnimation();
            WalkDownSprite.AddFrame(new Sprite(character, _downX, _downY, _downWidth, _downHeight), 0);
            WalkDownSprite.AddFrame(new Sprite(character, _downX + _downWidth, _downY, _downWidth, _downHeight), RUNNING_ANIMATION_LENGTH);
            WalkDownSprite.AddFrame(new Sprite(character, _downX + (2 * _downWidth), _downY, _downWidth, _downHeight), RUNNING_ANIMATION_LENGTH * 2);
            WalkDownSprite.AddFrame(new Sprite(character, _downX + (3 * _downWidth), _downY, _downWidth, _downHeight), RUNNING_ANIMATION_LENGTH * 3);
            WalkDownSprite.Play();
            WalkLeftSprite = new SpriteAnimation();
            WalkLeftSprite.AddFrame(new Sprite(character, _leftX, _leftY, _leftWidth, _leftHeight), 0);
            WalkLeftSprite.AddFrame(new Sprite(character, _leftX + _leftWidth, _leftY, _leftWidth, _leftHeight), RUNNING_ANIMATION_LENGTH);
            WalkLeftSprite.AddFrame(new Sprite(character, _leftX + (2 * _leftWidth), _leftY, _leftWidth, _leftHeight), RUNNING_ANIMATION_LENGTH * 2);
            WalkLeftSprite.AddFrame(new Sprite(character, _leftX + (3 * _leftWidth), _leftY, _leftWidth, _leftHeight), RUNNING_ANIMATION_LENGTH * 3);
            WalkLeftSprite.Play();
            WalkRightSprite = new SpriteAnimation();
            WalkRightSprite.AddFrame(new Sprite(character, _rightX, _rightY, _rightWidth, _rightHeight), 0);
            WalkRightSprite.AddFrame(new Sprite(character, _rightX + _rightWidth, _rightY, _rightWidth, _rightHeight), RUNNING_ANIMATION_LENGTH);
            WalkRightSprite.AddFrame(new Sprite(character, _rightX + (2 * _rightWidth), _rightY, _rightWidth, _rightHeight), RUNNING_ANIMATION_LENGTH * 2);
            WalkRightSprite.AddFrame(new Sprite(character, _rightX + (3 * _rightWidth), _rightY, _rightWidth, _rightHeight), RUNNING_ANIMATION_LENGTH * 3);
            WalkRightSprite.Play();

        }

        public void Update(GameTime gameTime)
        {
            switch (State)
            {
                case CharacterState.WalkUp:
                    WalkUpSprite.Update(gameTime);
                    break;
                case CharacterState.WalkDown:
                    WalkDownSprite.Update(gameTime);
                    break;
                case CharacterState.WalkLeft:
                    WalkLeftSprite.Update(gameTime);
                    break;
                case CharacterState.WalkRight:
                    WalkRightSprite.Update(gameTime);
                    break;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            switch (State)
            {
                case CharacterState.WalkUp:
                    WalkUpSprite.Draw(spriteBatch, Position);
                    break;
                case CharacterState.WalkDown:
                    WalkDownSprite.Draw(spriteBatch, Position);
                    break;
                case CharacterState.WalkLeft:
                    WalkLeftSprite.Draw(spriteBatch, Position);
                    break;
                case CharacterState.WalkRight:
                    WalkRightSprite.Draw(spriteBatch, Position);
                    break;
                default:
                    IdleSprite.Draw(spriteBatch, Position);
                    break;
            }
        }


    }
}