using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    public class BuildingManager : IGameEntity
    {
        public int DrawOrder => 3;
        
        public List<Building> Buildings = new List<Building>();

        private ContentManager _content;

        public BuildingManager(ContentManager content)
        {
            _content = content;
        }

        public void SpawnBuilding(String obstacle, Map map, Vector2 mapPosition, List<Rectangle> bounds, Rectangle entryPoint)
        {
            if (obstacle == null)
            {
                Buildings.Add(new Building(map, mapPosition, null, bounds, entryPoint));
            }
            else
            {
                Buildings.Add(new Building(map, mapPosition, _content.Load<Texture2D>(obstacle), bounds, entryPoint));
            }
        }


        public void ClearList()
        {
            Buildings = new List<Building>();
        }
        public void RemoveBuilding(Building building)
        {
            //removes Building from List of Building and maybe more?
        }

        public void Update(GameTime gameTime)
        {
            foreach (var building in Buildings)
            {
                building.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var building in Buildings)
            {
                
                building.Draw(gameTime, spriteBatch);
            }
        }
    }
}