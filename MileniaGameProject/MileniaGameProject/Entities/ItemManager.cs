using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    public class ItemManager : IGameEntity
    {
        public List<Item> Items;

        private ContentManager _content;
        
        public ItemManager(ContentManager content)
        {
            _content = content;
        }

        public void SpawnItem(String item)
        {
            //loads Item per string item
        }

        public void RemoveItem(Item item)
        {
            //removes item from List of Items and maybe more?
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