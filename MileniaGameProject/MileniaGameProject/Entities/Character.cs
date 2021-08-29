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
        public Sprite WalkUpSprite;
        public Sprite WalkDownSprite;
        public Sprite WalkLeftSprite;
        public Sprite WalkRightSprite;

        private int _idleX = 0;
        private int _idleY = 5;
        private int _idleWidth = 80;
        private int _idleHeight = 155;
        
        private int _upX = 80;
        private int _upY = 10;
        private int _upWidth = 85;
        private int _upHeight = 150;
        
        private int _downX = 165;
        private int _downY = 10;
        private int _downWidth = 85;
        private int _downHeight = 150;
        
        private int _leftX = 250;
        private int _leftY = 25;
        private int _leftWidth = 120;
        private int _leftHeight = 135;
        
        private int _rightX = 370;
        private int _rightY = 25;
        private int _rightWidth = 120;
        private int _rightHeight = 135;

        public CharacterState State = CharacterState.Idle;

        public Vector2 Position;

        public Rectangle CollisionBox
        {
            get
            {
                int width, height;
                switch (State)
                {
                    case CharacterState.WalkUp:
                        width = WalkUpSprite.Width;
                        height = WalkUpSprite.Height;
                        break;
                    case CharacterState.WalkDown:
                        width = WalkDownSprite.Width;
                        height = WalkDownSprite.Height;
                        break;
                    case CharacterState.WalkLeft:
                        width = WalkLeftSprite.Width;
                        height = WalkLeftSprite.Height;
                        break;
                    case CharacterState.WalkRight:
                        width = WalkRightSprite.Width;
                        height = WalkRightSprite.Height;
                        break;
                    default:
                        width = IdleSprite.Width;
                        height = IdleSprite.Height;
                        break;
                }

                Rectangle box = new Rectangle((int) Position.X, (int) Position.Y, width, height);
                return box;
            }
        }

        public Character(Texture2D character, Vector2 pos)
        {
            Position = pos;

            IdleSprite = new Sprite(character, _idleX, _idleY, _idleWidth, _idleHeight);
            WalkUpSprite = new Sprite(character, _upX, _upY, _upWidth, _upHeight);
            WalkDownSprite = new Sprite(character, _downX, _downY, _downWidth, _downHeight);
            WalkLeftSprite = new Sprite(character, _leftX, _leftY, _leftWidth, _leftHeight);
            WalkRightSprite = new Sprite(character, _rightX, _rightY, _rightWidth, _rightHeight);

        }

        public void Update(GameTime gameTime)
        {
            
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