using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    public class Map
    {
        private Texture2D _map;

        public Map(Texture2D map)
        {
            _map = map;
        }

        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_map, new Rectangle(0, 0, 1200, 900), Color.White);
            spriteBatch.End();
        }
    }
}