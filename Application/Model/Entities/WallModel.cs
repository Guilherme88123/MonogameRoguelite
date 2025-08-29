using Microsoft.Xna.Framework;
using MonogameRoguelite.Model.Entities.Base;

namespace MonogameRoguelite.Model.Entities;

public class WallModel : BaseEntityModel
{
    public static int DefaultSize = 64;

    public WallModel((float x, float y) position, (float width, float height) size) : base(position)
    {
        Size = new Vector2(size.width, size.height);
        Color = new Color(103, 71, 54);
        IsCollidable = false;
    }

    public WallModel((float x, float y) position) : this(position, (DefaultSize, DefaultSize)) 
    { 
    }
}
