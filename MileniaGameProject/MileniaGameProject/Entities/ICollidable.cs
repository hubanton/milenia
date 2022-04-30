using Microsoft.Xna.Framework;

namespace MileniaGameProject.Entities
{
    /// <summary>
    /// objects ingame that have a single rectangle as a hitbox implement this
    /// </summary>
    public interface ICollidable
    {
        Rectangle CollisionBox { get; }

        void CheckCollisions();
    }
}