using System;
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

        public List<(Rectangle, String)> EntryPoints;

        public Map(Texture2D mapTexture, Character character, List<(Rectangle, String)> entryPoints, Vector2 cameraPosition)
        {
            MapTexture = mapTexture;
            CameraPosition = cameraPosition;
            Character = character;
            EntryPoints = entryPoints;
        }

        public void Update(GameTime gameTime)
        {
            PlayerPosition = CameraPosition + Character.Position;
            CheckEntryPoints();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(MapTexture, Vector2.Zero, new Rectangle((int) CameraPosition.X, (int) CameraPosition.Y, Milenia.DefaultWidth, Milenia.DefaultHeight), Color.White);
        }

        private void CheckEntryPoints()
        {
            Rectangle sprite = Character.CollisionBox;
            int width = sprite.Width;
            int height = sprite.Height;
            
            if (EntryPoints != null)
            {
                foreach (var entryPoint in EntryPoints)
                {
                    if (new Rectangle((int) PlayerPosition.X, (int) PlayerPosition.Y, width,
                        height).Intersects(entryPoint.Item1))
                    {
                        if (entryPoint.Item2.Equals("TowerMap"))
                        {
                            Character.Position = new Vector2(0, (Milenia.DefaultHeight - height) / 2);
                            Milenia.MapManager.LoadMap(entryPoint.Item2, Character, null, new Vector2(0, 450));
                        } else {
                            Character.Position = new Vector2(Milenia.DefaultWidth - width, (Milenia.DefaultHeight - height) / 2);
                            Milenia.MapManager.LoadMap(entryPoint.Item2, Character, null, new Vector2(2000, 450));
                        }
                       
                    }
                }
            }
        }
    }
}