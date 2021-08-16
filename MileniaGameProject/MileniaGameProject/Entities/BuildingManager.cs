using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    public class BuildingManager : IGameEntity
    {
        public int DrawOrder => 1;
        
        public List<Building> Buildings;

        private ContentManager _content;

        public BuildingManager(ContentManager content)
        {
            _content = content;
        }

        public void SpawnBuilding(String building)
        {
            //loads Building per string building
        }

        public void RemoveBuilding(Building building)
        {
            //removes Building from List of Building and maybe more?
        }

        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new System.NotImplementedException();
        }
    }
}