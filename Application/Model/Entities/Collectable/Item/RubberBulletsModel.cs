using Application.Enum;
using Application.Model.Entities.Collectable.Item.Base;
using MonogameRoguelite.Dto;

namespace Application.Model.Entities.Collectable.Item;

public class RubberBulletsModel : BaseItemModel
{
    public RubberBulletsModel((float x, float y) position) : base(position)
    {
        Rarity = RarityType.Rare;
    }

    protected override void Apply()
    {
        GlobalVariables.Player.HasRicochetBullets = true;
    }

    protected override void Remove()
    {
        GlobalVariables.Player.HasRicochetBullets = false;
    }
}
