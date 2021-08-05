using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    public class Character
    {
        private Texture2D _char;

        public Character(Texture2D character)
        {
            _char = character;
        }

        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_char, new Vector2(500, 450), null, Color.White, 0f,
                Vector2.Zero, 0.25f, SpriteEffects.None, 0f);
            spriteBatch.End();
        }
    }
}