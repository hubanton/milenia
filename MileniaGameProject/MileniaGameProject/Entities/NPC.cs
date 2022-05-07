using System;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    /// <summary>
    /// class for npcs that can be interacted with
    /// </summary>
    public class NPC : IGameEntity, ICollidable
    {
        public int DrawOrder => 5;

        private readonly Map _map;
        private readonly Vector2 _mapPosition;
        private readonly Texture2D _npcTexture;
        private readonly Rectangle _bound;
        private readonly SpriteFont _npcFont;
        private readonly Texture2D _dialogBox;
        private readonly int _dialogBoxOffset = 15;
        private readonly Texture2D _npcPortrait;
        private readonly string _textToShow;
        public bool CanTalkTo = false;
        public bool IsTalking = false;

        public Rectangle CollisionBox
        {
            get
            {
                Rectangle box = new Rectangle((int) Math.Round(_mapPosition.X - _map.CameraPosition.X + _bound.X),
                    (int) Math.Round(_mapPosition.Y - _map.CameraPosition.Y + _bound.Y), _bound.Width,
                    _bound.Height);

                return box;
            }
        }

        public NPC(Map map, Vector2 mapPosition, Texture2D npcTexture, SpriteFont npcFont, Texture2D dialogBox, Texture2D npcPortrait)
        {
            _map = map;
            _mapPosition = mapPosition;
            _npcTexture = npcTexture;
            _bound = new Rectangle(0, 0, npcTexture.Width, npcTexture.Height);
            _npcFont = npcFont;
            _dialogBox = dialogBox;
            _npcPortrait = npcPortrait;
            // find better way to load text
            using(StreamReader reader = new StreamReader(@"C:\Users\Willi\Desktop\milenia\MileniaGameProject\MileniaGameProject\Content\Dialog\JoeText.txt"))
            {
                _textToShow = reader.ReadToEnd();
            }
        }

        public void Update(GameTime gameTime)
        {
            CheckCollisions();
            CheckInteractable();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_npcTexture, new Vector2((int) (_mapPosition.X - _map.CameraPosition.X),
                (int) (_mapPosition.Y - _map.CameraPosition.Y)), Color.White);
            if (IsTalking)
            {
                spriteBatch.Draw(_dialogBox, new Vector2((float) (Milenia.DefaultWidth / 5), Milenia.DefaultHeight * 2 / 3 - _dialogBoxOffset), Color.White);
                spriteBatch.Draw(_npcPortrait, new Vector2((float) (Milenia.DefaultWidth / 5) + 685, Milenia.DefaultHeight * 2 / 3 + 20 - _dialogBoxOffset), Color.White);
                spriteBatch.DrawString(_npcFont, _textToShow, new Vector2(Milenia.DefaultWidth / 5 + 40, Milenia.DefaultHeight * 2 / 3 + 40 - _dialogBoxOffset), Color.Black, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
            }
        }

        /// <summary>
        /// checks if player collides with that npc and prevents the player from further moving in that direction
        /// </summary>
        public void CheckCollisions()
        {
            Rectangle npcCollisionBox = CollisionBox;
            Rectangle characterCollisionBox = _map.Character.CollisionBox;

            if (npcCollisionBox.Intersects(characterCollisionBox))
            {
                Rectangle tempRect = Rectangle.Intersect(npcCollisionBox, characterCollisionBox);

                if (tempRect.Width <= tempRect.Height)
                {
                    if (tempRect.X > characterCollisionBox.X)
                    {
                        _map.CanMoveRight = false;
                    }
                    else
                    {
                        _map.CanMoveLeft = false;
                    }
                }
                else
                {
                    if (tempRect.Y > characterCollisionBox.Y)
                    {
                        _map.CanMoveDown = false;
                    }
                    else
                    {
                        _map.CanMoveUp = false;
                    }
                }
            }
        }

        /// <summary>
        /// checks if player is in range to interact with npc
        /// </summary>
        private void CheckInteractable()
        {
            Point npcPoint = CollisionBox.Center;
            Vector2 npcPos = new Vector2(npcPoint.X, npcPoint.Y);
            Point charPoint = _map.Character.CollisionBox.Center;
            Vector2 charPos = new Vector2(charPoint.X, charPoint.Y);
            if (Vector2.Distance(npcPos, charPos) < 200)
            {
                CanTalkTo = true;
            }
            else
            {
                CanTalkTo = false;
            }
        }
    }
}