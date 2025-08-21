using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Teste001.Dto;
using Teste001.Model.Entities.Base;
using Teste001.Model.Entities.Creature.Base;
using Teste001.Model.Entities.Drop.Coin;
using Teste001.Model.Entities.Drop.Xp;

namespace Teste001.Model.Entities.Creature.Enemy.Base;

public abstract class BaseEnemyModel : BaseCreatureModel
{
    public BaseEnemyModel((int x, int y) position, int maxHealt) : base(position, maxHealt)
    {
    }

    public override void Update(GameTime gameTime, List<BaseEntityModel> entities)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        var speedX = Speed.X;
        var speedY = Speed.Y;

        if (Position.X <= 0 || Position.X >= GlobalVariables.Graphics.PreferredBackBufferWidth - Size)
            speedX *= -1;
        if (Position.Y <= 0 || Position.Y >= GlobalVariables.Graphics.PreferredBackBufferHeight - Size)
            speedY *= -1;

        Speed = new Vector2(speedX, speedY);

        Position += Speed * delta;

        base.Update(gameTime, entities);
    }

    protected override Dictionary<Type, int> Drops()
    {
        return new()
        {
            { typeof(CoinModel), 1 },
            { typeof(XpNodeModel), 1 },
        };
    }
}
