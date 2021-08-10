using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    public class Character : IGameEntity, ICollidable
    {
        public Texture2D CharTexture;

        public Vector2 Position;

        public Rectangle CollisionBox
        {
            get
            {
                Rectangle box = new Rectangle((int) Position.X, (int) Position.Y, CharTexture.Width, CharTexture.Height);
                return box;
            }
            
        }

        public Character(Texture2D character, Vector2 pos)
        {
            Position = pos;
            CharTexture = character;
            Position.Y -= character.Height / 2;
            Position.X -= character.Width / 2;
            
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CharTexture, Position, Color.White);
        }


    }
}