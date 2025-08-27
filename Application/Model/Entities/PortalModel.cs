using MonogameRoguelite.Model.Entities.Base;
using Microsoft.Xna.Framework;

namespace Application.Model.Entities;

public class PortalModel : BaseEntityModel
{
    public PortalModel((float x, float y) position) : base(position)
    {
        Size = new(256, 256);
        Color = Color.Purple;
    }
}
