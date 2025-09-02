using Application.Enum;
using Application.Model.Entities.Bullet;
using Application.Model.Entities.Collectable.Gun;
using Application.Model.Entities.Collectable.Gun.Base;
using Application.Model.Entities.Creature.Enemy.Ranged.Base;
using Application.Model.Entities.Drop.Heart;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities;
using MonogameRoguelite.Model.Entities.Base;
using MonogameRoguelite.Model.Entities.Drop.Xp;
using System;
using System.Collections.Generic;

namespace Application.Model.Entities.Creature.Enemy.Ranged;

public class FlyModel : BaseRangedEnemyModel
{
    public FlyModel((float x, float y) position) : base(position, 3)
    {
        Size = new Vector2(32, 32);
        Acceleration = 500f;
        Friction = 300f;
        MaxSpeed = 100f;
        Color = Color.DarkGray;

        Gun = new EnemyGunModel((0, 0));
        Gun.User = this;
    }

    protected override Dictionary<Type, (int, int)> Drops()
    {
        return new()
        {
            { typeof(HeartModel), (0, 1) },
            { typeof(XpNodeModel), (1, 2) },
        };
    }
}
