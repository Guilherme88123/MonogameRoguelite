using Application.Enum;
using Application.Model.Entities.Collectable.Item.Base;
using MonogameRoguelite.Dto;

namespace Application.Model.Entities.Collectable.Item;

public class HeartCanisterModel : BaseItemModel
{
    private int QuantityExtraLife = 2;

    public HeartCanisterModel((float x, float y) position) : base(position)
    {
        Rarity = RarityType.Common;
    }

    protected override void Apply()
    {
        GlobalVariables.Player.MaxHealth += QuantityExtraLife;
    }

    protected override void Remove()
    {
        GlobalVariables.Player.MaxHealth -= QuantityExtraLife;
    }
}
