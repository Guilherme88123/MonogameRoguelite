using Application.Model.Entities.Bullet;
using Application.Model.Entities.Collectable.Gun.Base;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Application.Model.Entities.Collectable.Gun;

public class EnemyGunModel : BaseGunModel
{
    public EnemyGunModel((float x, float y) position) : base(position)
    {
        Delay = 2f;
        Size = new(32, 12);
        Color = Color.DarkSlateGray;
        Name = "Enemy Gun";
    }

    protected override Dictionary<Type, int> Bullets()
    {
        return new()
        {
            { typeof(EnemyBulletModel), 1 },
        };
    }
}
