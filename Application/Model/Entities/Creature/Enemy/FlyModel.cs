using Application.Model.Entities.Drop.Heart;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities;
using MonogameRoguelite.Model.Entities.Base;
using MonogameRoguelite.Model.Entities.Creature.Enemy.Base;
using MonogameRoguelite.Model.Entities.Drop.Coin;
using MonogameRoguelite.Model.Entities.Drop.Xp;
using System;
using System.Collections.Generic;

namespace Application.Model.Entities.Creature.Enemy;

public class FlyModel : BaseEnemyModel
{
    public const float DelayTiro = 2f;
    public float DelayTiroAtual { get; set; } = DelayTiro;

    public FlyModel((int x, int y) position) : base(position, 3)
    {
        Size = new Vector2(32, 32);
        Acceleration = 500f;
        Friction = 300f;
        MaxSpeed = 100f;
        Color = Color.DarkGray;
        VisionRange = 500f;
    }

    public override void Update(GameTime gameTime, List<BaseEntityModel> entities)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        DelayTiroAtual -= delta;

        if (DelayTiroAtual <= 0 && MoveStatus == Enum.MoveType.Chase)
        {
            var direction = GlobalVariables.PlayerPosition - Position;
            entities.Add(new BulletModel(((int)(Position.X + Size.X / 2), (int)(Position.Y + Size.Y / 2)), direction, this));
            DelayTiroAtual = DelayTiro;
        }

        base.Update(gameTime, entities);
    }

    protected override Dictionary<Type, int> Drops()
    {
        return new()
        {
            { typeof(HeartModel), 1 },
            { typeof(XpNodeModel), 1 },
        };
    }
}
