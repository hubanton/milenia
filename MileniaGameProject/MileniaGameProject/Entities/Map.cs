using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    public class Map
    {
        public Texture2D MapTexture;
        public Vector2 CameraPosition;
        public Vector2 PlayerPosition;
        public Character Character;

        public Map(Texture2D mapTexture, Character character)
        {
            MapTexture = mapTexture;
            CameraPosition = Vector2.Zero;
            Character = character;
        }

        public void Update(GameTime gameTime)
        {
            PlayerPosition = CameraPosition + Character.Position;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(MapTexture, Vector2.Zero, new Rectangle((int) CameraPosition.X, (int) CameraPosition.Y, Milenia.DefaultWidth, Milenia.DefaultHeight), Color.White);
        }
    }
}