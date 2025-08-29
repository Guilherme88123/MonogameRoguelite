using Application.Enum;
using Application.Model.Entities.Bullet;
using Application.Model.Entities.Collectable.Gun.Base;
using System;
using System.Collections.Generic;

namespace Application.Model.Entities.Collectable.Gun;

public class MachineGunModel : BaseGunModel
{
    public MachineGunModel((float x, float y) position) : base(position)
    {
        Size = new(48, 12);
        Delay = 0.1f;
        Color = Microsoft.Xna.Framework.Color.Gray;
        Rarity = RarityType.Legendary;
        BulletSpeedFactor = 0.8f;
    }

    protected override Dictionary<Type, int> Bullets()
    {
        return new()
        {
            { typeof(PrimaryBulletModel), 1 },
        };
    }
}
