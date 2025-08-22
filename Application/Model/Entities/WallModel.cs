using Microsoft.Xna.Framework;
using MonogameRoguelite.Model.Entities.Base;

namespace MonogameRoguelite.Model.Entities;

public class WallModel : BaseEntityModel
{
    public WallModel((int x, int y) position) : base(position)
    {
        Size = new Vector2(64, 64);
        Color = Color.Brown;
    }
}
