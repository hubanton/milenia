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
            Position = new Vector2(0, 0);
        }

        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(MapTexture, Vector2.Zero, new Rectangle((int) Position.X, (int) Position.Y, Milenia.DefaultWidth, Milenia.DefaultHeight), Color.White);
        }
    }
}