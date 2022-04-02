using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    /// <summary>
    /// interface that all entities and managers ingame have to implement
    /// </summary>
    public interface IGameEntity
    {
        // used to determine in what order to draw the entities
        // (sorted from small to big and drawn in that order)
        public int DrawOrder { get; }

        public void Update(GameTime gameTime);

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}