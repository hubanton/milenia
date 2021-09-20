using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    public abstract class Obstacle : IGameEntity, ICollidables
    {
        public int DrawOrder => 4;
        
        protected Map _map;
        protected Vector2 _mapPosition;
        protected Texture2D _obstacleTexture;
        protected List<Rectangle> _bounds;

        public virtual List<Rectangle> CollisionBox
        {
            get
            {
                List<Rectangle> box = new List<Rectangle>();
                foreach (Rectangle bound in _bounds)
                {
                    box.Add(new Rectangle((int) Math.Round(_mapPosition.X - _map.CameraPosition.X + bound.X),
                        (int) Math.Round(_mapPosition.Y - _map.CameraPosition.Y + bound.Y), bound.Width,
                        bound.Height));
                }

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


        protected virtual void CheckCollisions()
        {
            List<Rectangle> obstacleCollisionBox = CollisionBox;
            Rectangle characterCollisionBox = _map.Character.CollisionBox;

            foreach (var collisionBox in obstacleCollisionBox)
            {
                if (collisionBox.Intersects(characterCollisionBox))
                {
                    Rectangle tempRect = Rectangle.Intersect(collisionBox, characterCollisionBox);

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
}