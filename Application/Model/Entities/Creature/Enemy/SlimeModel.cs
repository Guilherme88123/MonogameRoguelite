using Microsoft.Xna.Framework;
using Teste001.Model.Entities.Creature.Enemy.Base;

namespace Teste001.Model.Entities.Creature.Enemy;

public class SlimeModel : BaseEnemyModel
{
    public SlimeModel((int x, int y) position) : base(position, 5)
    {
        Size = 48;
        Speed = new Vector2(70, 70);
        Color = Color.Red;
    }
}
