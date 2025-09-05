using Application.Model.Entities.Collectable.Base;
using MonogameRoguelite.Dto;

namespace Application.Model.Entities.Collectable.Item.Base;

public abstract class BaseItemModel : BaseCollectableModel
{
    public BaseItemModel((float x, float y) position) : base(position)
    {
    }

    protected virtual void Apply()
    {
    }

    protected virtual void Remove()
    {
    }
}
