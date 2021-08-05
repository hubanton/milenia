using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGame.Entities
{
    public interface IGameEntity
    {

        public void Update(GameTime gameTime);

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}