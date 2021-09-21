using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    public class Asset : Obstacle
    {
        private int _sheetX, _sheetY, _sheetWidth, _sheetHeight;



        public Asset(Map map, Vector2 mapPosition, Texture2D obstacleTexture, int sheetX, int sheetY, int? sheetWidth, int? sheetHeight, List<Rectangle> bounds) : base(map, mapPosition, obstacleTexture)
        {
            _sheetX = sheetX;
            _sheetY = sheetY;
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

            _bounds = bounds;
        }
        
      

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_obstacleTexture, new Vector2((int) (_mapPosition.X - _map.CameraPosition.X),
                (int) (_mapPosition.Y - _map.CameraPosition.Y)), new Rectangle(_sheetX, _sheetY, _sheetWidth, _sheetHeight), Color.White);
        }


     
    }
}