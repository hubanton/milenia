using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    public abstract class Obstacle : IGameEntity, ICollidables
    {
        /// <summary>
        /// abstract class from which all objects inherit
        /// </summary>
        public int DrawOrder => 4;
        
        protected readonly Map Map;
        protected Vector2 MapPosition;
        protected readonly Texture2D ObstacleTexture;
        protected List<Rectangle> Bounds;

        /// <summary>
        /// returns a list of bounds to check collision with player
        /// </summary>
        public virtual List<Rectangle> CollisionBox
        {
            get
            {
                List<Rectangle> box = new List<Rectangle>();
                foreach (Rectangle bound in Bounds)
                {
                    box.Add(new Rectangle((int) Math.Round(MapPosition.X - Map.CameraPosition.X + bound.X),
                        (int) Math.Round(MapPosition.Y - Map.CameraPosition.Y + bound.Y), bound.Width,
                        bound.Height));
                }

                return box;
            }
        }

        /// <summary>
        /// gets extendend by sub classes to include more attributes and specific knowledge
        /// </summary>
        /// <param name="map"></param>
        /// <param name="mapPosition"></param>
        /// <param name="obstacleTexture"></param>
        protected Obstacle(Map map, Vector2 mapPosition, Texture2D obstacleTexture)
        {
            Map = map;
            MapPosition = mapPosition;
            ObstacleTexture = obstacleTexture;
        }

        public virtual void Update(GameTime gameTime)
        {
            CheckCollisions();
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);


        /// <summary>
        /// checks if player collides with that obstacle and prevents the player from further moving in that direction
        /// </summary>
        public virtual void CheckCollisions()
        {
            List<Rectangle> obstacleCollisionBox = CollisionBox;
            Rectangle characterCollisionBox = Map.Character.CollisionBox;

            foreach (var collisionBox in obstacleCollisionBox)
            {
                if (collisionBox.Intersects(characterCollisionBox))
                {
                    Rectangle tempRect = Rectangle.Intersect(collisionBox, characterCollisionBox);

                    if (tempRect.Width <= tempRect.Height)
                    {
                        if (tempRect.X > characterCollisionBox.X)
                        {
                            Map.CanMoveRight = false;
                        }
                        else
                        {
                            Map.CanMoveLeft = false;
                        }
                    }
                    else
                    {
                        if (tempRect.Y > characterCollisionBox.Y)
                        {
                            Map.CanMoveDown = false;
                        }
                        else
                        {
                            Map.CanMoveUp = false;
                        }
                    }
                }
            }
            
        }
    }
}