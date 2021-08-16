using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    public class Building : Obstacle
    {
        public int DrawOrder => 1;
        
        protected List<Rectangle> _bounds;
        
        public override List<Rectangle> CollisionBox
        {
            get
            {
                List<Rectangle> box = new List<Rectangle>();
                foreach (var bound in _bounds)
                {
                    box.Add(new Rectangle((int) Math.Round(_mapPosition.X - _map.CameraPosition.X + bound.X),
                        (int) Math.Round(_mapPosition.Y - _map.CameraPosition.Y + bound.Y), bound.Width,
                        bound.Height));
                }

                return box;
            }
        }

        public Building(Map map, Vector2 mapPosition, Texture2D obstacleTexture, List<Rectangle> bounds) : base(map, mapPosition, obstacleTexture)
        {
            if (bounds != null) _bounds = bounds;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_obstacleTexture, new Vector2((int) (_mapPosition.X - _map.CameraPosition.X),
                (int) (_mapPosition.Y - _map.CameraPosition.Y)), Color.White);
        }

        private void CheckCollisions()
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