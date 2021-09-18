using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MileniaGameProject.UserInput;

namespace MileniaGameProject.Entities
{
    public class MapManager : IGameEntity
    {
        public int DrawOrder => 0;
        
        public Map Map;
        
        private ContentManager _content;
        
        private InputController _inputController;

        public MapManager(ContentManager content)
        {
            _content = content;
        }

        public void LoadMap(String map, Character character, List<(Rectangle, String)> entryPoints, Vector2 cameraPosition)
        {
            Map = new Map(_content.Load<Texture2D>(map), character, entryPoints, cameraPosition);
            if (Map.MapTexture.Name == "TowerMap")
            {
                List<Rectangle> archBounds = new List<Rectangle>();
                archBounds.Add(new Rectangle(40, 765, 200, 175));
                archBounds.Add(new Rectangle(400, 765, 200, 175));
                Milenia.BuildingManager.SpawnBuilding("Arch", Map, new Vector2(680, 600), archBounds, Rectangle.Empty);
                List<Rectangle> invisibleBounds = new List<Rectangle>()
                {
                    new Rectangle(0, 0, 175, 5600),
                    new Rectangle(0, 3000, 700, 2500), // ok
                    new Rectangle(0, 0, 700, 1800),
                    new Rectangle(0, 0, 400, 1934),
                    new Rectangle(1300, 0, 700, 6300), // ok
                    new Rectangle(0, 6100, 1300, 200), // ok
                    new Rectangle(700, 0, 600, 550) // ok
                    
                    
                };
                Milenia.BuildingManager.SpawnBuilding(null, Map, new Vector2(0, 0), invisibleBounds, Rectangle.Empty);
            }
            else
            {
                Milenia.BuildingManager.ClearList();
            }

            _inputController = new InputController(character, Map);
        }

        public void Update(GameTime gameTime)
        {
            //Activates Input Listener for KeyboardControls
            _inputController.ProcessControls(gameTime, Map);
            
            Map.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Map.Draw(gameTime, spriteBatch);
        }
    }
}