using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Teste001.Model.Entities.Creature.Enemy.Base;
using Teste001.Model.Entities.Drop.Coin;
using Teste001.Model.Entities.Drop.Xp;

namespace Teste001.Model.Entities.Creature.Enemy.Boss;

public class KingSlimeModel : BaseEnemyModel
{
    public KingSlimeModel((int x, int y) position) : base(position, 30)
    {
        Size = 128;
        Speed = new Vector2(50, 50);
        Color = Color.DarkRed;
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
