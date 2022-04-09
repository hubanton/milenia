using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MileniaGameProject.Graphics;
using MileniaGameProject.SupportFiles;
using MileniaGameProject.UserInput;

namespace MileniaGameProject.Entities
{
    public class Character : IGameEntity, ICollidable
    {
        public int DrawOrder => 2;

        public Sprite IdleSprite;
        public SpriteAnimation WalkUpSprite;
        public SpriteAnimation WalkDownSprite;
        public SpriteAnimation WalkLeftSprite;
        public SpriteAnimation WalkRightSprite;

        public int NumLoops = 0;

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

        public static int RUNNING_ANIMATION_FRAME_DURATION = 6;

        public CharacterState State = CharacterState.Idle;

        public Vector2 Position;

        public int HasJustLoaded = 0;

        public Rectangle CollisionBox
        {
            get
            {
                int width, height;
                switch (State)
                {
                   
                    case(CharacterState.WalkUp):
                        width = WalkUpSprite.CurrentFrame.Sprite.Width; height = WalkUpSprite.CurrentFrame.Sprite.Height;
                        break;
                    case(CharacterState.WalkDown):
                        width = WalkDownSprite.CurrentFrame.Sprite.Width; height = WalkDownSprite.CurrentFrame.Sprite.Height;
                        break;
                    case(CharacterState.WalkLeft):
                        width = WalkLeftSprite.CurrentFrame.Sprite.Width; height = WalkLeftSprite.CurrentFrame.Sprite.Height;
                        break;
                    case(CharacterState.WalkRight):
                        width = WalkRightSprite.CurrentFrame.Sprite.Width; height = WalkRightSprite.CurrentFrame.Sprite.Height;
                        break;
                    default: 
                        width = IdleSprite.Width; height = IdleSprite.Height;
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
            WalkUpSprite = new SpriteAnimation();
            WalkUpSprite.AddFrame(new Sprite(character, _upX, _upY, _upWidth, _upHeight), 0);
            WalkUpSprite.AddFrame(new Sprite(character, _upX + _upWidth, _upY, _upWidth, _upHeight), RUNNING_ANIMATION_FRAME_DURATION);
            WalkUpSprite.AddFrame(new Sprite(character, _upX + (2 * _upWidth), _upY, _upWidth, _upHeight), RUNNING_ANIMATION_FRAME_DURATION * 2);
            WalkUpSprite.AddFrame(new Sprite(character, _upX + (3 * _upWidth), _upY, _upWidth, _upHeight), RUNNING_ANIMATION_FRAME_DURATION * 3);
            WalkUpSprite.Play();
            WalkDownSprite = new SpriteAnimation();
            WalkDownSprite.AddFrame(new Sprite(character, _downX, _downY, _downWidth, _downHeight), 0);
            WalkDownSprite.AddFrame(new Sprite(character, _downX + _downWidth, _downY, _downWidth, _downHeight), RUNNING_ANIMATION_FRAME_DURATION);
            WalkDownSprite.AddFrame(new Sprite(character, _downX + (2 * _downWidth), _downY, _downWidth, _downHeight), RUNNING_ANIMATION_FRAME_DURATION * 2);
            WalkDownSprite.AddFrame(new Sprite(character, _downX + (3 * _downWidth), _downY, _downWidth, _downHeight), RUNNING_ANIMATION_FRAME_DURATION * 3);
            WalkDownSprite.Play();
            WalkLeftSprite = new SpriteAnimation();
            WalkLeftSprite.AddFrame(new Sprite(character, _leftX, _leftY, _leftWidth, _leftHeight), 0);
            WalkLeftSprite.AddFrame(new Sprite(character, _leftX + _leftWidth, _leftY, _leftWidth, _leftHeight), RUNNING_ANIMATION_FRAME_DURATION);
            WalkLeftSprite.AddFrame(new Sprite(character, _leftX + (2 * _leftWidth), _leftY, _leftWidth, _leftHeight), RUNNING_ANIMATION_FRAME_DURATION * 2);
            WalkLeftSprite.AddFrame(new Sprite(character, _leftX + (3 * _leftWidth), _leftY, _leftWidth, _leftHeight), RUNNING_ANIMATION_FRAME_DURATION * 3);
            WalkLeftSprite.Play();
            WalkRightSprite = new SpriteAnimation();
            WalkRightSprite.AddFrame(new Sprite(character, _rightX, _rightY, _rightWidth, _rightHeight), 0);
            WalkRightSprite.AddFrame(new Sprite(character, _rightX + _rightWidth, _rightY, _rightWidth, _rightHeight), RUNNING_ANIMATION_FRAME_DURATION);
            WalkRightSprite.AddFrame(new Sprite(character, _rightX + (2 * _rightWidth), _rightY, _rightWidth, _rightHeight), RUNNING_ANIMATION_FRAME_DURATION * 2);
            WalkRightSprite.AddFrame(new Sprite(character, _rightX + (3 * _rightWidth), _rightY, _rightWidth, _rightHeight), RUNNING_ANIMATION_FRAME_DURATION * 3);
            WalkRightSprite.Play();

        }

        public void Update(GameTime gameTime)
        {
            if (CurHealth < 1000 && NumLoops == 1)
            {
                NumLoops = 0;
                ++CurHealth;
            }
            else
            {
                ++NumLoops;
            }
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
            } else
            {
                    IdleSprite.Draw(spriteBatch, Position);
            } 
            
        }


    }
}