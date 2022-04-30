using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    /// <summary>
    /// default class for obstacles that have hitboxes and cannot be interacted with
    /// </summary>
    public class Asset : Obstacle
    {
        public override int DrawOrder => 2;
        
        private int _sheetX, _sheetY, _sheetWidth, _sheetHeight;



        public Asset(Map map, Vector2 mapPosition, Texture2D obstacleTexture, int sheetX, int sheetY, int? sheetWidth, int? sheetHeight, List<Rectangle> bounds) : base(map, mapPosition, obstacleTexture)
        {
            _sheetX = sheetX;
            _sheetY = sheetY;
            
            Bounds = bounds;
            
            if (obstacleTexture == null)
            {
                _sheetWidth = 0;
                _sheetHeight = 0;
                return;
            }

            if (sheetWidth != null)
            {
                _sheetWidth = (int) sheetWidth;
            }
            else
            {
                _sheetWidth = obstacleTexture.Width;
            }

            if (sheetHeight != null)
            {
                _sheetHeight = (int) sheetHeight;
            }
            else
            {
                _sheetHeight = obstacleTexture.Height;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (ObstacleTexture != null)
            {
                spriteBatch.Draw(ObstacleTexture, new Vector2((int) (MapPosition.X - Map.CameraPosition.X),
                        (int) (MapPosition.Y - Map.CameraPosition.Y)),
                    new Rectangle(_sheetX, _sheetY, _sheetWidth, _sheetHeight), Color.White);
            }
        }


     
    }
}