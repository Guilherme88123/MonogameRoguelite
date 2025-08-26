using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using MonogameRoguelite.Model.Entities.Creature.Enemy.Base;
using MonogameRoguelite.Model.Entities.Drop.Coin;
using MonogameRoguelite.Model.Entities.Drop.Xp;

namespace MonogameRoguelite.Model.Entities.Creature.Enemy.Boss;

public class KingSlimeModel : BaseEnemyModel
{
    public KingSlimeModel((float x, float y) position) : base(position, 30)
    {
        Size = new Vector2(128, 128);
        Acceleration = 600f;
        Friction = 320f;
        MaxSpeed = 120f;
        Color = Color.DarkRed;
        VisionRange = 1000f;
    }

    protected override Dictionary<Type, int> Drops()
    {
        return new()
        {
            { typeof(CoinModel), 3 },
            { typeof(XpNodeModel), 2 },
        };
    }
}
