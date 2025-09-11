using Application.Enum;
using Application.Model.Entities.Bullet;
using Application.Model.Entities.Collectable.Gun.Base;
using System;
using System.Collections.Generic;

namespace Application.Model.Entities.Collectable.Gun;

public class UziModel : BaseGunModel
{
    public UziModel((float x, float y) position) : base(position)
    {
        Size = new(40, 10);
        Delay = 0.2f;
        Color = Microsoft.Xna.Framework.Color.DarkBlue;
        Rarity = RarityType.Rare;
        BulletSpeedFactor = 0.6f;
        Name = "Uzi";
    }

    protected override Dictionary<Type, int> Bullets()
    {
        return new()
        {
            { typeof(PrimaryBulletModel), 1 },
        };
    }
}
