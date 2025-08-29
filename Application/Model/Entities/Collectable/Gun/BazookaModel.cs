using Application.Enum;
using Application.Model.Entities.Bullet;
using Application.Model.Entities.Collectable.Gun.Base;
using System;
using System.Collections.Generic;

namespace Application.Model.Entities.Collectable.Gun;

public class BazookaModel : BaseGunModel
{
    public BazookaModel((float x, float y) position) : base(position)
    {
        Size = new(96, 16);
        Delay = 2.3f;
        Color = Microsoft.Xna.Framework.Color.OrangeRed;
        Rarity = RarityType.Secret;
    }

    protected override Dictionary<Type, int> Bullets()
    {
        return new()
        {
            { typeof(BazookaBulletModel), 1 },
        };
    }
}
