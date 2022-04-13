using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    /// <summary>
    /// Manager of all buildings
    /// </summary>
    public class BuildingManager : IGameEntity
    {
        public int DrawOrder => 3;

        private List<Building> _buildings = new List<Building>();

        private ContentManager _content;

        public BuildingManager(ContentManager content)
        {
            _content = content;
        }

        /// <summary>
        /// spawns a building
        /// throws an exception if no string for texture is given
        /// </summary>
        /// <param name="obstacle"></param>
        /// <param name="map"></param>
        /// <param name="mapPosition"></param>
        /// <param name="bounds"></param>
        /// <param name="entryPoint"></param>
        public void SpawnBuilding(String obstacle, Map map, Vector2 mapPosition, List<Rectangle> bounds, Rectangle entryPoint)
        {
            if (obstacle == null)
            {
                throw new NullReferenceException();
            }

            _buildings.Add(new Building(map, mapPosition, _content.Load<Texture2D>(obstacle), bounds, entryPoint));
        }


        public void ClearList()
        {
            _buildings = new List<Building>();
        }
        
        public void RemoveBuilding(Building building)
        {
            //removes Building from List of Building and maybe more?
        }

        public void Update(GameTime gameTime)
        {
            foreach (var building in _buildings)
            {
                building.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var building in _buildings)
            {
                
                building.Draw(gameTime, spriteBatch);
            }
        }
    }
}