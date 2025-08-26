using Application.Enum;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities.Base;
using MonogameRoguelite.Model.Entities.Creature.Base;
using MonogameRoguelite.Model.Entities.Drop.Coin;
using MonogameRoguelite.Model.Entities.Drop.Xp;

namespace MonogameRoguelite.Model.Entities.Creature.Enemy.Base;

public abstract class BaseEnemyModel : BaseCreatureModel
{
    public Vector2 Target { get; set; }
    public float VisionRange { get; set; }

    public const float DelayIdle = 1.5f;
    public float DelayIdleAtual { get; set; }

    public BaseEnemyModel((int x, int y) position, int maxHealt) : base(position, maxHealt)
    {
        ChooseNewTarget();
    }

    public override void Update(GameTime gameTime, List<BaseEntityModel> entities)
    {
        switch (MoveStatus)
        {
            case MoveType.Idle:
                HandleIdle(gameTime);
                break;
            case MoveType.Patrol:
                HandlePatrol(gameTime);
                break;
            case MoveType.Chase:
                HandleChase(gameTime);
                break;
        }

        if (Vector2.Distance(Position, GlobalVariables.Player.Position) < VisionRange)
        {
            MoveStatus = MoveType.Chase;
        }

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

    public override void Colision(BaseEntityModel model)
    {
        if (model is WallModel wall && Speed.LengthSquared() < 0.01f)
        {
            ChooseNewTarget();
        }

        base.Colision(model);
    }

    private void HandleIdle(GameTime gameTime)
    {
        DelayIdleAtual -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (DelayIdleAtual <= 0)
        {
            ChooseNewTarget();
            MoveStatus = MoveType.Patrol;
        }
    }

    private void HandlePatrol(GameTime gameTime)
    {
        MoveTowards(Target, gameTime);
        TargetDirection = Target;

        if (Vector2.Distance(Position, Target) < 5f)
        {
            DelayIdleAtual = DelayIdle;
            MoveStatus = MoveType.Idle;
        }
    }

    private void HandleChase(GameTime gameTime)
    {
        var playerPos = GlobalVariables.Player.Position;

        if (playerPos == Vector2.Zero)
        {
            DelayIdleAtual = DelayIdle;
            MoveStatus = MoveType.Idle;

            return;
        }

        MoveTowards(playerPos, gameTime);

        if (Vector2.Distance(Position, playerPos) > VisionRange)
        {
            DelayIdleAtual = DelayIdle;
            MoveStatus = MoveType.Idle;
        }
    }

    private void MoveTowards(Vector2 target, GameTime gameTime)
    {
        Vector2 direction = target - Position;
        TargetDirection = direction;
        if (direction.LengthSquared() > 0.01f)
        {
            Move(direction, (float)gameTime.ElapsedGameTime.TotalSeconds);
        }
    }

    private void ChooseNewTarget()
    {
        Target = new Vector2(
            new Random().Next(50, GlobalVariables.Graphics.PreferredBackBufferWidth - 50), // limites da sala
            new Random().Next(50, GlobalVariables.Graphics.PreferredBackBufferHeight - 50)
        );
    }


}
