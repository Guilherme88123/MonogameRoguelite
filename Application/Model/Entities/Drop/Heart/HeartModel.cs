using Microsoft.Xna.Framework;
using MonogameRoguelite.Model.Entities.Drop.Base;

namespace Application.Model.Entities.Drop.Heart;

public class HeartModel : BaseDropModel
{
    public HeartModel((int x, int y) position) : base(position)
    {
        Size = new Vector2(24, 24);
        Color = Color.Red;
        Value = 1;
    }
}
