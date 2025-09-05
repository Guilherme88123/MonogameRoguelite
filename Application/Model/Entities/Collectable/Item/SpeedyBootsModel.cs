using Application.Enum;
using Application.Model.Entities.Collectable.Item.Base;
using MonogameRoguelite.Dto;

namespace Application.Model.Entities.Collectable.Item;

public class SpeedyBootsModel : BaseItemModel
{
    private float QuantintyExtraSpeed = 30;

    public SpeedyBootsModel((float x, float y) position) : base(position)
    {
        Rarity = RarityType.Uncommon;
    }

    protected override void Apply()
    {
        GlobalVariables.Player.Acceleration += QuantintyExtraSpeed;
        GlobalVariables.Player.MaxSpeed += QuantintyExtraSpeed;
    }

    protected override void Remove()
    {
        GlobalVariables.Player.Acceleration -= QuantintyExtraSpeed;
        GlobalVariables.Player.MaxSpeed -= QuantintyExtraSpeed;
    }
}
