using Teste001.Model.Entities.Base;
using Teste001.Model.Entities.Creature.Enemy.Base;

namespace Teste001.Model.Entities;

public class WallModel : BaseEntityModel
{
    public WallModel((int x, int y) position) : base(position)
    {
        Size = 16;
        Color = Microsoft.Xna.Framework.Color.DarkOrange;
    }
}
