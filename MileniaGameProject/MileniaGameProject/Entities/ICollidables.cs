using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MileniaGameProject.Entities
{
    /// <summary>
    /// objects ingame that have multiple rectangles as a hitbox implement this
    /// </summary>
    public interface ICollidables
    {
        List<Rectangle> CollisionBox { get; }
    }
}