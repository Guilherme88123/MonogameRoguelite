using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Teste001.Model.Entities.Base;

namespace Teste001.Model.Entities.Drop.Base;

public class BaseDropModel : BaseEntityModel
{
    public int Value { get; set; }

    public BaseDropModel((int x, int y) position) : base(position)
    {
        MaxSpeed = 150f;
        Acceleration = 1500f;
        Friction = 300f;
        Direction = new Vector2(Random.Shared.Next(-1, 2), Random.Shared.Next(-1, 2));
    }

    public override void Update(GameTime gameTime, List<BaseEntityModel> entities)
    {
        var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        Move(Direction, delta);

        if (Speed.Length() >= MaxSpeed)
            Direction = Vector2.Zero;

        base.Update(gameTime, entities);
    }
}
