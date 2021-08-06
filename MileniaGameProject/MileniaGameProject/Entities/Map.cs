﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    public class Map
    {
        private Texture2D _map;
        public Vector2 Position;

        public Map(Texture2D map)
        {
            _map = map;
            Position = new Vector2(-600, -450);
        }

        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_map, Position, Color.White);
            spriteBatch.End();
        }
    }
}