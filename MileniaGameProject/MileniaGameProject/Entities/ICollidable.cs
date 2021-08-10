using Microsoft.Xna.Framework;

namespace MileniaGameProject.Entities
{
    public interface ICollidable
    {
        Rectangle CollisionBox { get; }
    }
}