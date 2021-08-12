using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    public abstract class Obstacle : IGameEntity, ICollidable
    {
        protected Map _map;
        protected Vector2 _mapPosition;
        protected Texture2D _obstacleTexture;

        public virtual Rectangle CollisionBox
        {
            get
            {
                Rectangle box = new Rectangle((int) Math.Round(_mapPosition.X - _map.CameraPosition.X),
                    (int) Math.Round(_mapPosition.Y - _map.CameraPosition.Y), _obstacleTexture.Width,
                    _obstacleTexture.Height);
                return box;
            }
        }

        public Obstacle(Map map, Vector2 mapPosition, Texture2D obstacleTexture)
        {
            _map = map;
            _mapPosition = mapPosition;
            _obstacleTexture = obstacleTexture;
        }

        public virtual void Update(GameTime gameTime)
        {
            CheckCollisions();
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        


        private void CheckCollisions()
        {
            Rectangle obstacleCollisionBox = CollisionBox;
            Rectangle characterCollisionBox = _map.Character.CollisionBox;

            if (obstacleCollisionBox.Intersects(characterCollisionBox))
            {
                Rectangle tempRect = Rectangle.Intersect(obstacleCollisionBox, characterCollisionBox);

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
    }
}