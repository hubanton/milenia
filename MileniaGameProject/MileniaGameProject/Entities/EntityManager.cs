using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    /// <summary>
    /// Manager of all mananagers
    /// calls their update and draw functions at every frame
    /// </summary>
    public class EntityManager
    {
        /// <summary>
        /// list containing all managers to call their draw and update functions
        /// </summary>
        private readonly List<IGameEntity> _entities = new List<IGameEntity>();
        
        private readonly List<IGameEntity> _entitiesToAdd = new List<IGameEntity>();
        private readonly List<IGameEntity> _entitiesToRemove = new List<IGameEntity>();
        
        public IEnumerable<IGameEntity> Entities => new ReadOnlyCollection<IGameEntity>(_entities);
        
        public void Update(GameTime gameTime)
        {
            foreach (var entity in _entitiesToRemove)
            {
                _entities.Remove(entity);
            }

            foreach (var entity in _entities)
            {
                entity.Update(gameTime);    
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach(IGameEntity entity in _entities.OrderBy(e => e.DrawOrder))
            {

                entity.Draw(gameTime, spriteBatch);

            }

        }

        public void AddEntity(IGameEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity), "Null cannot be added as an entity.");

            _entities.Add(entity);

        }

        public void RemoveEntity(IGameEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity), "You can't delete something that doesn't exist.");

            _entitiesToRemove.Add(entity);

        }

        public void Clear()
        {

            _entitiesToRemove.AddRange(_entities);

        }

        public IEnumerable<T> GetEntitiesOfType<T>() where T : IGameEntity
        {
            return _entities.OfType<T>();
        }
    }
}