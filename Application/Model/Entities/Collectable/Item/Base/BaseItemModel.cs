using Application.Model.Entities.Collectable.Base;

namespace Application.Model.Entities.Collectable.Item.Base;

public abstract class BaseItemModel : BaseCollectableModel
{
    public BaseItemModel((float x, float y) position) : base(position)
    {
    }
}
