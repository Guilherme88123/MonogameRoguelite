using Application.Enum;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities.Base;
using MonogameRoguelite.Model.Entities.Creature.Base;
using MonogameRoguelite.Model.Entities.Drop.Coin;
using MonogameRoguelite.Model.Entities.Drop.Xp;
using Application.Model;

namespace MonogameRoguelite.Model.Entities.Creature.Enemy.Base;

public abstract class BaseEnemyModel : BaseCreatureModel
{
    public Vector2 Target { get; set; }

    public const float DelayIdle = 1.5f;
    public float DelayIdleAtual { get; set; }

    public BaseEnemyModel((float x, float y) position, int maxHealt) : base(position, maxHealt + (int)((maxHealt * 0.5) * (GlobalVariables.Flor - 1)))
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

        if (MoveStatus != MoveType.Chase && CanSeePlayer())
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

        var tileSize = (int)WallModel.Size.X;

        var playerPoint = new Point((int)playerPos.X / tileSize, (int)playerPos.Y / tileSize);
        var enemyPoint = new Point((int)Position.X / tileSize, (int)Position.Y / tileSize);

        List<Point> path = FindPath(enemyPoint, playerPoint);

        Console.WriteLine($"Path count: {path.Count}");

        if (path.Count > 1)
        {
            Point nextStep = path[1];

            Vector2 targetPos = new Vector2(nextStep.X * WallModel.Size.X, nextStep.Y * WallModel.Size.Y);

            if (nextStep.X == enemyPoint.X) targetPos.X = Position.X;
            if (nextStep.Y == enemyPoint.Y) targetPos.Y = Position.Y;

            MoveTowards(targetPos, gameTime);
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

    public bool CanSeePlayer()
    {
        var walls = GlobalVariables.CurrentRoom.Walls;

        Vector2 direction = GlobalVariables.Player.Position - Position;
        float distance = direction.Length();
        direction.Normalize();

        float step = 16f;
        Vector2 currentPos = Position;

        for (float i = 0; i < distance; i += step)
        {
            currentPos += direction * step;

            foreach (var wall in walls)
            {
                if (wall == null) continue;

                if (wall.Rectangle.Contains(currentPos.ToPoint()))
                    return false;
            }
        }

        return true;
    }
}
