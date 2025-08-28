using Application.Model.Entities.Bullet;
using Application.Model.Entities.Collectable.Gun.Base;
using System;
using System.Collections.Generic;

namespace Application.Model.Entities.Collectable.Gun;

public class ShotgunModel : BaseGunModel
{
    public ShotgunModel((float x, float y) position) : base(position)
    {
        Size = new(64, 12);
        Delay = 1f;
        Rarity = Enum.RarityType.Epic;
        Color = Microsoft.Xna.Framework.Color.SaddleBrown;
    }

    protected override Dictionary<Type, int> Bullets()
    {
        return new()
        {
            { typeof(PrimaryBulletModel), 5 },
        };
    }
}
