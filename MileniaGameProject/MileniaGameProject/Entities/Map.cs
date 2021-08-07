using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    public class Map
    {
        public Texture2D MapTexture;
        public Vector2 Position;

        public Map(Texture2D mapTexture, Vector2 position)
        {
            MapTexture = mapTexture;
            Position = position;
        }

        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(MapTexture, Position, new Rectangle(100, 100, 1600, 900), Color.White);
        }
    }
}