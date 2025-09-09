using Application.Model.Entities.Collectable.Item.Base;

namespace Application.Model.Entities.Collectable.Item;

public class TwinsBulletModel : BaseItemModel
{
    public TwinsBulletModel((float x, float y) position) : base(position)
    {
        Rarity = Enum.RarityType.Legendary;
        Color = Microsoft.Xna.Framework.Color.Coral;
        Name = "Twins Bullet";
    }
}
