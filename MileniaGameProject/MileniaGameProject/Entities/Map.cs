using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    public class Map
    {
        public int DrawOrder => 0;
        
        public bool canMoveUp = true;
        public bool canMoveDown = true;
        public bool canMoveLeft = true;
        public bool canMoveRight = true;
        
        
        public Texture2D MapTexture;
        public Vector2 CameraPosition;
        
        public Character Character;
        public Vector2 PlayerPosition;

        public List<Rectangle> EntryPoints;

        public Map(Texture2D mapTexture, Character character, List<Rectangle> entryPoints)
        {
            MapTexture = mapTexture;
            CameraPosition = Vector2.Zero;
            Character = character;
            EntryPoints = entryPoints;
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