using Application.Model.Entities.Bullet;
using Application.Model.Entities.Collectable.Gun.Base;
using MonogameRoguelite.Model.Entities.Base;
using System;
using System.Collections.Generic;

namespace Application.Model.Entities.Collectable.Gun;

public class PrimaryGunModel : BaseGunModel
{
    public PrimaryGunModel((float x, float y) position) : base(position)
    {
    }

    protected override Dictionary<Type, int> Bullets()
    {
        return new()
        {
            { typeof(PrimaryBulletModel), 1 },
        };
    }
}
