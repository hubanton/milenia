using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    public class Building : Obstacle
    {
        public int DrawOrder => 1;
        
        protected Rectangle _bounds;
        
        public override Rectangle CollisionBox
        {
            get
            {
                Rectangle box = new Rectangle((int) Math.Round(_mapPosition.X - _map.CameraPosition.X + _bounds.X),
                    (int) Math.Round(_mapPosition.Y - _map.CameraPosition.Y + _bounds.Y), _bounds.Width,
                    _bounds.Height);
                return box;
            }
        }

        public Building(Map map, Vector2 mapPosition, Texture2D obstacleTexture, Rectangle? bounds) : base(map, mapPosition, obstacleTexture)
        {
            if (bounds != null) _bounds = (Rectangle) bounds;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_obstacleTexture, new Vector2((int) (_mapPosition.X - _map.CameraPosition.X),
                (int) (_mapPosition.Y - _map.CameraPosition.Y)), Color.White);
        }

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