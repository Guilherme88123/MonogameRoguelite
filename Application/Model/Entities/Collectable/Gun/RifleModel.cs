using Application.Enum;
using Application.Model.Entities.Bullet;
using Application.Model.Entities.Collectable.Gun.Base;
using System;
using System.Collections.Generic;

namespace Application.Model.Entities.Collectable.Gun;

public class RifleModel : BaseGunModel
{
    public RifleModel((float x, float y) position) : base(position)
    {
        Size = new(48, 12);
        Delay = 0.35f;
        Color = Microsoft.Xna.Framework.Color.Firebrick;
        Rarity = RarityType.Uncommon;
        Name = "Rifle";
    }

    protected override Dictionary<Type, int> Bullets()
    {
        return new()
        {
            { typeof(PrimaryBulletModel), 1 },
        };
    }
}
