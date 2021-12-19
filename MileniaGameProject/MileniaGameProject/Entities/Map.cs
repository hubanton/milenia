using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
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

        private int offsetWidth;
        private int offsetHeight;
        private int drawWidth;
        private int drawHeight;
        
        public Character Character;
        public Vector2 PlayerPosition;

        public List<(Rectangle, String)> EntryPoints;

        public Map(Texture2D mapTexture, Character character, List<(Rectangle, String)> entryPoints,
            Vector2 cameraPosition)
        {
            MapTexture = mapTexture;
            CameraPosition = cameraPosition;
            Character = character;
            EntryPoints = entryPoints;
            offsetWidth = mapTexture.Width < Milenia.DefaultWidth ? (Milenia.DefaultWidth - mapTexture.Width) / 2 : 0;
            offsetHeight = mapTexture.Height < Milenia.DefaultHeight ? (Milenia.DefaultHeight - mapTexture.Height) / 2 : 0;
            drawWidth = Math.Min(mapTexture.Width, Milenia.DefaultWidth);
            drawHeight = Math.Min(mapTexture.Height, Milenia.DefaultHeight);
        }

        public void Update(GameTime gameTime)
        {
            PlayerPosition = CameraPosition + Character.Position;
            CheckEntryPoints();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(MapTexture, new Vector2(offsetWidth, offsetHeight),
                new Rectangle((int) CameraPosition.X, (int) CameraPosition.Y, drawWidth,
                    drawHeight), Color.White);
        }

        [SuppressMessage("ReSharper.DPA", "DPA0003: Excessive memory allocations in LOH", MessageId = "type: System.Byte[]")]
        private void CheckEntryPoints()
        {
            Rectangle sprite = Character.CollisionBox;
            int width = sprite.Width;
            int height = sprite.Height;

            if (EntryPoints != null && Character.hasJustLoaded == 0)
            {
                foreach (var entryPoint in EntryPoints)
                {
                    if (new Rectangle((int) PlayerPosition.X, (int) PlayerPosition.Y, width,
                        height).Intersects(entryPoint.Item1))
                    {
                        if (entryPoint.Item2.Equals("TowerMap"))
                        {
                            List<(Rectangle, string)> entries = new List<(Rectangle, string)>();
                            Vector2 camPos;
                            entries.Add((new Rectangle(0, 5700, 1, 100), "PlayerBaseProto"));
                            entries.Add((new Rectangle(874, 450, 248, 80), "TowerEntrance"));
                            if (Character.Position.X > 800)
                            {
                                Character.Position = new Vector2(0, (Milenia.DefaultHeight - height) / 2);
                                Character.Position.X += 10;
                                camPos = new Vector2(0, 5300);
                            }
                            else
                            {
                                Character.Position = new Vector2((Milenia.DefaultWidth - width) / 2, (Milenia.DefaultHeight - height) / 2);
                                Character.Position.X += 10;
                                camPos = new Vector2(200, 200);
                            }

                            Milenia.MapManager.LoadMap(entryPoint.Item2, Character, entries, camPos);
                        }
                        else if (entryPoint.Item2.Equals("TownMap"))
                        {
                            List<(Rectangle, string)> entries = new List<(Rectangle, string)>();
                            entries.Add((new Rectangle(6399, 3610, 1, 100), "PlayerBaseProto"));
                            Character.Position = new Vector2(Milenia.DefaultWidth - width,
                                (Milenia.DefaultHeight - height) / 2);
                            Character.Position.X -= 10;
                            Milenia.MapManager.LoadMap(entryPoint.Item2, Character, entries, new Vector2(4800, 3200));
                        } 
                        else if (entryPoint.Item2.Equals("TowerEntrance"))
                        {
                            List<(Rectangle, string)> entries = new List<(Rectangle, string)>();
                            entries.Add((new Rectangle(525, 1190, 250, 10), "TowerMap"));
                            Character.Position = new Vector2((Milenia.DefaultWidth - width) / 2,
                                Milenia.DefaultHeight - height);
                            Character.Position.Y -= 10;
                            Milenia.MapManager.LoadMap(entryPoint.Item2, Character, entries, new Vector2(0, 300));
                        }
                        else
                        {
                            List<(Rectangle, string)> entries = new List<(Rectangle, string)>();
                            entries.Add((new Rectangle(0, 580, 1, 100), "TownMap"));
                            entries.Add((new Rectangle(2399, 800, 1, 100), "TowerMap"));
                            Character.Position = new Vector2(1600 - Character.Position.X - width, (Milenia.DefaultHeight - height) / 2);
                            Vector2 camPos;
                            if (Character.Position.X < 500)
                            {
                                camPos = new Vector2(0, 250);
                            }
                            else
                            {
                                camPos = new Vector2(800, 400);
                                Character.Position.X -= 1;
                            }

                            Milenia.MapManager.LoadMap(entryPoint.Item2, Character, entries, camPos);
                        }
                    }
                }

                Character.hasJustLoaded = 15;
            }
        }
    }
}