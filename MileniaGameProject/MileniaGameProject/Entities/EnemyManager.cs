using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    public class EnemyManager : IGameEntity
    {
        public List<Enemy> Enemies;

        private ContentManager _content;

        public EnemyManager(ContentManager content)
        {
            _content = content;
        }

        public void SpawnEnemy(String enemy)
        {
            //loads Enemy per string enemy
        }

        public void RemoveEnemy(Enemy enemy)
        {
            //removes Enemy from List of Enemies and maybe more?
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