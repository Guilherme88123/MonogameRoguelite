using Microsoft.Xna.Framework;
using MonogameRoguelite.Model.Entities.Drop.Base;

namespace MonogameRoguelite.Model.Entities.Drop.Xp;

public class XpNodeModel : BaseDropModel
{
    public XpNodeModel((int x, int y) position) : base(position)
    {
        Size = new Vector2(16, 16);
        Color = Color.Purple;
        Value = 1;
    }
}
