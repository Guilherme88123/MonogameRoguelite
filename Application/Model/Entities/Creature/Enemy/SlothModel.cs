using Application.Model.Entities.Drop.Heart;
using Microsoft.Xna.Framework;
using MonogameRoguelite.Model.Entities.Creature.Enemy.Base;
using MonogameRoguelite.Model.Entities.Drop.Coin;
using MonogameRoguelite.Model.Entities.Drop.Xp;
using System;
using System.Collections.Generic;

namespace Application.Model.Entities.Creature.Enemy;

public class SlothModel : BaseEnemyModel
{
    public SlothModel((float x, float y) position) : base(position, 10)
    {
        Size = new Vector2(96, 96);
        Acceleration = 450f;
        Friction = 250f;
        MaxSpeed = 100f;
        Color = Color.SaddleBrown;
    }

    protected override Dictionary<Type, (int, int)> Drops()
    {
        return new()
        {
            { typeof(CoinModel), (1, 2) },
            { typeof(XpNodeModel), (1, 2) },
        };
    }
}
