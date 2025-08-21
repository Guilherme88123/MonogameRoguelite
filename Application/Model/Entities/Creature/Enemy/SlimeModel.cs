using Microsoft.Xna.Framework;
using Teste001.Model.Entities.Creature.Enemy.Base;

namespace Teste001.Model.Entities.Creature.Enemy;

public class SlimeModel : BaseEnemyModel
{
    public SlimeModel((int x, int y) position) : base(position, 5)
    {
        Size = new Vector2(48, 48);
        Acceleration = 750f;
        Friction = 400f;
        MaxSpeed = 150f;
        Color = Color.DarkGreen;
    }
}
