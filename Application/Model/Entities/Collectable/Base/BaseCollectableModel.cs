using Application.Enum;
using Microsoft.Xna.Framework;
using MonogameRoguelite.Model.Entities.Base;
using System;
using System.Collections.Generic;

namespace Application.Model.Entities.Collectable.Base;

public class BaseCollectableModel : BaseEntityModel
{
    public RarityType Rarity { get; set; } = RarityType.Common;

    public BaseCollectableModel((float x, float y) position) : base(position)
    {
        MaxSpeed = 200f;
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
