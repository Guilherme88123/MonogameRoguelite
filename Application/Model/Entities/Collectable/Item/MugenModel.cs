using Application.Model.Entities.Collectable.Item.Base;
using MonogameRoguelite.Dto;

namespace Application.Model.Entities.Collectable.Item;

public class MugenModel : BaseItemModel
{
    public MugenModel((float x, float y) position) : base(position)
    {
    }
    protected override void Apply()
    {
        GlobalVariables.Player.HasMugen = true;
    }

    protected override void Remove()
    {
        GlobalVariables.Player.HasMugen = false;
    }
}
