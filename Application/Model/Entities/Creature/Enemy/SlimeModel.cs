using Microsoft.Xna.Framework;
using MonogameRoguelite.Model.Entities.Creature.Enemy.Base;

namespace MonogameRoguelite.Model.Entities.Creature.Enemy;

public class SlimeModel : BaseEnemyModel
{
    public SlimeModel((float x, float y) position) : base(position, 5)
    {
        Size = new Vector2(48, 48);
        Acceleration = 750f;
        Friction = 400f;
        MaxSpeed = 150f;
        Color = Color.DarkGreen;
        VisionRange = 200f;
    }
}
