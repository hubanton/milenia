using System;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    public class NPC : IGameEntity, ICollidable
    {
        public int DrawOrder => 5;

        private Map _map;
        private Vector2 _mapPosition;
        private Texture2D _npcTexture;
        private Rectangle bound;
        private SpriteFont _npcFont;
        private Texture2D DialogBox;
        private string _textToShow;
        public bool CanTalkTo = false;
        public bool IsTalking = false;

        public Rectangle CollisionBox
        {
            get
            {
                Rectangle box = new Rectangle((int) Math.Round(_mapPosition.X - _map.CameraPosition.X + bound.X),
                    (int) Math.Round(_mapPosition.Y - _map.CameraPosition.Y + bound.Y), bound.Width,
                    bound.Height);

                return box;
            }
        }

        public NPC(Map map, Vector2 mapPosition, Texture2D npcTexture, SpriteFont npcFont, Texture2D dialogBox)
        {
            _map = map;
            _mapPosition = mapPosition;
            _npcTexture = npcTexture;
            bound = new Rectangle(0, 0, npcTexture.Width, npcTexture.Height);
            _npcFont = npcFont;
            DialogBox = dialogBox;
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
                spriteBatch.Draw(DialogBox, new Vector2((float) (Milenia.DefaultWidth / 5), Milenia.DefaultHeight * 2 / 3), Color.White);
                spriteBatch.DrawString(_npcFont, _textToShow, new Vector2(Milenia.DefaultWidth / 5 + 40, Milenia.DefaultHeight * 2 / 3 + 40), Color.Black, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
            }
        }

        protected virtual void CheckCollisions()
        {
            Rectangle npcCollisionBox = CollisionBox;
            Rectangle characterCollisionBox = _map.Character.CollisionBox;

            if (npcCollisionBox.Intersects(characterCollisionBox))
            {
                Rectangle tempRect = Rectangle.Intersect(npcCollisionBox, characterCollisionBox);

                if (tempRect.Width <= tempRect.Height)
                {
                    if (tempRect.X > _map.Character.Position.X)
                    {
                        _map.canMoveRight = false;
                    }
                    else
                    {
                        _map.canMoveLeft = false;
                    }
                }
                else
                {
                    if (tempRect.Y > _map.Character.Position.Y)
                    {
                        _map.canMoveDown = false;
                    }
                    else
                    {
                        _map.canMoveUp = false;
                    }
                }
            }
        }

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