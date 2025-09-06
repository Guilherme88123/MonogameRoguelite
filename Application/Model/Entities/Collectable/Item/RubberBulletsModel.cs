using Application.Enum;
using Application.Model.Entities.Collectable.Item.Base;
using MonogameRoguelite.Dto;

namespace Application.Model.Entities.Collectable.Item;

public class RubberBulletsModel : BaseItemModel
{
    public RubberBulletsModel((float x, float y) position) : base(position)
    {
        Rarity = RarityType.Rare;
        Color = Microsoft.Xna.Framework.Color.SandyBrown;
        Name = "Rubber Bullets";
    }
}
