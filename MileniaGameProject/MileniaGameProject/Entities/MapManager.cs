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
            Milenia.BuildingManager.ClearList();
            Milenia.ForegroundObstacleManager.ClearList();
            Milenia.BackgroundObstacleManager.ClearList();
            if (Map.MapTexture.Name == "TowerMap")
            {
                List<Rectangle> archBounds = new List<Rectangle>();
                archBounds.Add(new Rectangle(40, 765, 200, 175));
                archBounds.Add(new Rectangle(400, 765, 200, 175));
                Milenia.BuildingManager.SpawnBuilding("Arch", Map, new Vector2(680, 600), archBounds, Rectangle.Empty);
                List<Rectangle> invisibleBounds = new List<Rectangle>()
                {
                    new Rectangle(0, 0, 175, 5500),//passt
                    new Rectangle(0, 3100, 700, 2400), //passt
                    new Rectangle(0, 0, 700, 1800), // passt
                    new Rectangle(1300, 0, 700, 6300), //passt
                    new Rectangle(0, 6100, 2000, 200), //passt
                    new Rectangle(700, 0, 600, 550), //passt
                    new Rectangle(645, 2435, 727, 223) // passt
                    
                };
                Milenia.ForegroundObstacleManager.SpawnObstacle("tree1", Map, new Vector2(-45, 5847), null);
                Milenia.ForegroundObstacleManager.SpawnObstacle("tree2", Map, new Vector2(368, 5804), null);
                Milenia.ForegroundObstacleManager.SpawnObstacle("tree1", Map, new Vector2(667, 5844), null);
                Milenia.ForegroundObstacleManager.SpawnObstacle("tree1", Map, new Vector2(923, 5773), null);
                Milenia.ForegroundObstacleManager.SpawnObstacle("tree2", Map, new Vector2(649, 5119), null);
                Milenia.ForegroundObstacleManager.SpawnObstacle("tree1", Map, new Vector2(663, 4219), null);
                Milenia.ForegroundObstacleManager.SpawnObstacle("tree1", Map, new Vector2(617, 3327), null);
                Milenia.ForegroundObstacleManager.SpawnObstacle("tree2", Map, new Vector2(1116, 3391), null);
                Milenia.ForegroundObstacleManager.SpawnObstacle("tree1", Map, new Vector2(1143, 2502), null);
                Milenia.ForegroundObstacleManager.SpawnObstacle("tree2", Map, new Vector2(1116, 1524), null);
                Milenia.BackgroundObstacleManager.SpawnObstacle("stone1", Map, new Vector2(537, 5456), null);
                Milenia.BackgroundObstacleManager.SpawnObstacle("stone1", Map, new Vector2(588, 2591), null);
                Milenia.BackgroundObstacleManager.SpawnObstacle("bush1", Map, new Vector2(1210, 5420), null);
                Milenia.BackgroundObstacleManager.SpawnObstacle("bush1", Map, new Vector2(1225, 673), null);
                Milenia.BackgroundObstacleManager.SpawnObstacle("bush1", Map, new Vector2(636, 606), null);
                Milenia.BackgroundObstacleManager.SpawnObstacle("bush1", Map, new Vector2(140, 2017), null);
                Milenia.BackgroundObstacleManager.SpawnObstacle("bush1", Map, new Vector2(1231, 3151), null);
                Milenia.BackgroundObstacleManager.SpawnObstacle("stone1", Map, new Vector2(1190, 4403), null);
                Milenia.BackgroundObstacleManager.SpawnObstacle("bush1", Map, new Vector2(650, 4862), null);
                Milenia.BackgroundObstacleManager.SpawnObstacle("bush1", Map, new Vector2(1179, 4987), null);
                Milenia.ForegroundObstacleManager.SpawnObstacle("pillar1", Map, new Vector2(538, 4918), null);
                Milenia.ForegroundObstacleManager.SpawnObstacle("pillar2", Map, new Vector2(1300, 4968), null);
                Milenia.ForegroundObstacleManager.SpawnObstacle("pillar3", Map, new Vector2(538, 3784), null);
                Milenia.ForegroundObstacleManager.SpawnObstacle("pillar4", Map, new Vector2(1300, 3784), null);
                Milenia.ForegroundObstacleManager.SpawnObstacle("pillar5", Map, new Vector2(538, 2884), null);
                Milenia.ForegroundObstacleManager.SpawnObstacle("pillar6", Map, new Vector2(1300, 3165), null);
                Milenia.ForegroundObstacleManager.SpawnObstacle("pillar7", Map, new Vector2(538, 1984), null);
                Milenia.ForegroundObstacleManager.SpawnObstacle("pillar8", Map, new Vector2(1300, 1984), null);
                Milenia.BackgroundObstacleManager.SpawnObstacle("statue", Map, new Vector2(888, 284), null);
                
                Milenia.BuildingManager.SpawnBuilding(null, Map, new Vector2(0, 0), invisibleBounds, Rectangle.Empty);
            }
            else if (Map.MapTexture.Name == "TownMap")
            {
                List<Rectangle> settlerBounds = new List<Rectangle>();
                settlerBounds.Add(new Rectangle(75, 285, 1010, 315));
                Milenia.BuildingManager.SpawnBuilding("SettlerHouse", Map, new Vector2(5186, 2500), settlerBounds, Rectangle.Empty);
                List<Rectangle> survavilistBounds = new List<Rectangle>();
                survavilistBounds.Add(new Rectangle(28, 230, 456, 260));
                Milenia.BuildingManager.SpawnBuilding("SurvavilistHouse", Map, new Vector2(5645, 320), survavilistBounds, Rectangle.Empty);
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