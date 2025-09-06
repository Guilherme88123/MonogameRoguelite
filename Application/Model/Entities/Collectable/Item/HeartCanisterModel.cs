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
        Color = Microsoft.Xna.Framework.Color.DarkRed;
        Name = "Heart Canister";
    }

    public override void Apply()
    {
        GlobalVariables.Player.MaxHealth += QuantityExtraLife;
    }

    public override void Remove()
    {
        GlobalVariables.Player.MaxHealth -= QuantityExtraLife;
    }
}
