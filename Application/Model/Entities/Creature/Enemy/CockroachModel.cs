using Microsoft.Xna.Framework;
using MonogameRoguelite.Model.Entities.Creature.Enemy.Base;
using MonogameRoguelite.Model.Entities.Drop.Coin;
using MonogameRoguelite.Model.Entities.Drop.Xp;
using System;
using System.Collections.Generic;

namespace Application.Model.Entities.Creature.Enemy;

public class CockroachModel : BaseEnemyModel
{
    public CockroachModel((float x, float y) position) : base(position, 2)
    {
        Size = new Vector2(32, 32);
        Acceleration = 1000f;
        Friction = 600f;
        MaxSpeed = 200f;
        Color = Color.Brown;
    }

    protected override Dictionary<Type, (int, int)> Drops()
    {
        return new()
        {
            { typeof(CoinModel), (0, 1) },
            { typeof(XpNodeModel), (0, 1) },
        };
    }
}
