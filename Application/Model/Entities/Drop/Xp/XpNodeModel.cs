using Microsoft.Xna.Framework;
using Teste001.Model.Entities.Drop.Base;

namespace Teste001.Model.Entities.Drop.Xp;

public class XpNodeModel : BaseDropModel
{
    public XpNodeModel((int x, int y) position) : base(position)
    {
        Size = 16;
        Color = Color.Purple;
        Value = 1;
    }
}
