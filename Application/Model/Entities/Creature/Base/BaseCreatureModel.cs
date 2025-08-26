using Application.Enum;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities.Base;

namespace MonogameRoguelite.Model.Entities.Creature.Base;

public abstract class BaseCreatureModel : BaseEntityModel
{
    public int MaxHealth { get; set; }
    public int Health { get; set; }
    public MoveType MoveStatus { get; set; } = MoveType.Idle;
    public Vector2 TargetDirection { get; set; } = new();

    public BaseCreatureModel((int x, int y) position, int maxHealth) : base(position)
    {
        MaxHealth = maxHealth;
        Health = MaxHealth;
        TargetDirection = Direction;
    }

    public override void Update(GameTime gameTime, List<BaseEntityModel> entities)
    {
        if (Health <= 0)
        {
            Destroy();
        }

        base.Update(gameTime, entities);
    }
}
