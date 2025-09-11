using Application.Model.Entities.Bullet;
using Application.Model.Entities.Collectable.Gun.Base;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Application.Model.Entities.Collectable.Gun;

public class PistolModel : BaseGunModel
{
    public PistolModel((float x, float y) position) : base(position)
    {
        Size = new(32, 8);
        Color = Color.Black;
        Name = "Pistol";
    }

    protected override Dictionary<Type, int> Bullets()
    {
        return new()
        {
            { typeof(PrimaryBulletModel), 1 },
        };
    }
}
