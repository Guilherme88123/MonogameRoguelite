using Application.Enum;
using Application.Model.Entities.Bullet;
using Application.Model.Entities.Collectable.Gun.Base;
using System;
using System.Collections.Generic;

namespace Application.Model.Entities.Collectable.Gun;

public class SniperModel : BaseGunModel
{
    public SniperModel((float x, float y) position) : base(position)
    {
        Size = new(96, 12);
        Delay = 1.5f;
        Color = Microsoft.Xna.Framework.Color.MediumPurple;
        Rarity = RarityType.Mythic;
        Name = "Sniper";
    }

    protected override Dictionary<Type, int> Bullets()
    {
        return new()
        {
            { typeof(SniperBulletModel), 1 },
        };
    }
}
