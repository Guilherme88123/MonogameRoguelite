using MonogameRoguelite.Model.Entities.Base;

namespace Application.Model.Entities.Collectable.Base;

public class BaseCollectableModel : BaseEntityModel
{
    public BaseCollectableModel((float x, float y) position) : base(position)
    {
    }
}
