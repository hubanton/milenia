﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    /// <summary>
    /// special obstacle that has entrypoints
    /// </summary>
    public class Building : Obstacle
    {

        public override int DrawOrder {
            get
            {
                if (Milenia.Character.Position.Y < MapPosition.Y - Map.CameraPosition.Y + _layerThreshold)
                {
                    return 4;
                }

                return 2;
            }
        }
        
        private Rectangle _entryPoint;

        private int _layerThreshold;

        /// <summary>
        /// property that correctly retrieves the rectangle location on the screen
        /// </summary>
        public Rectangle EntryPoint =>
            new Rectangle((int) Math.Round(MapPosition.X - Map.CameraPosition.X + _entryPoint.X),
                (int) Math.Round(MapPosition.Y - Map.CameraPosition.Y + _entryPoint.Y), _entryPoint.Width,
                _entryPoint.Height);
        

        public Building(Map map, Vector2 mapPosition, Texture2D obstacleTexture, List<Rectangle> bounds, Rectangle entryPoint, int layerThreshold) : base(map, mapPosition, obstacleTexture)
        {
            if (bounds != null) Bounds = bounds;
            _entryPoint = entryPoint;
            _layerThreshold = layerThreshold;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (ObstacleTexture != null)
            {
                spriteBatch.Draw(ObstacleTexture, new Vector2((int) (MapPosition.X - Map.CameraPosition.X),
                    (int) (MapPosition.Y - Map.CameraPosition.Y)), Color.White);
            }
        }

        /// <summary>
        /// overridden CheckCollision to account for entrypoints and load a new map appriopiately
        /// </summary>
        public override void CheckCollisions()
        {
            List<Rectangle> obstacleCollisionBox = CollisionBox;
            Rectangle characterCollisionBox = Map.Character.CollisionBox;

            Rectangle doorCollisionBox = EntryPoint;
            
            if (characterCollisionBox.Intersects(doorCollisionBox))
            {
                // replace with generic LoadInterior type method
                // Milenia.ObstacleManager.ClearList();
                // Milenia.BuildingManager.ClearList();
                // Milenia.MapManager.LoadMap("interior", _map.Character, null);
            }
            else
            {
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
}