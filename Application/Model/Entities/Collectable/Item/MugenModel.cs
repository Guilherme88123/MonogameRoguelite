using Application.Enum;
using Application.Model.Entities.Collectable.Item.Base;
using MonogameRoguelite.Dto;

namespace Application.Model.Entities.Collectable.Item;

public class MugenModel : BaseItemModel
{
    public MugenModel((float x, float y) position) : base(position)
    {
        Rarity = RarityType.Epic;
        Color = Microsoft.Xna.Framework.Color.Purple;
        Name = "Mugen";
    }
}
