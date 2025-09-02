using Application.Model.Entities.Collectable.Gun;
using Application.Model.Entities.Creature.Enemy.Ranged.Base;
using Application.Model.Entities.Drop.Heart;
using Microsoft.Xna.Framework;
using MonogameRoguelite.Model.Entities.Drop.Coin;
using MonogameRoguelite.Model.Entities.Drop.Xp;
using System;
using System.Collections.Generic;

namespace Application.Model.Entities.Creature.Enemy.Ranged;

public class RatModel : BaseRangedEnemyModel
{
    public RatModel((float x, float y) position) : base(position, 5)
    {
        Size = new Vector2(48, 56);
        Acceleration = 450f;
        Friction = 280f;
        MaxSpeed = 90f;
        Color = Color.DimGray;

        Gun = new RifleModel((0, 0));
        Gun.User = this;
    }

    protected override Dictionary<Type, (int, int)> Drops()
    {
        return new()
        {
            { typeof(HeartModel), (0, 1) },
            { typeof(XpNodeModel), (1, 2) },
            { typeof(CoinModel), (1, 2) },
        };
    }
}
