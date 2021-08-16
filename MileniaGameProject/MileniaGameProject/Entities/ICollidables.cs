using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MileniaGameProject.Entities
{
    public interface ICollidables
    {
        List<Rectangle> CollisionBox { get; }
    }
}