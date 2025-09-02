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
    public Point FinalTarget { get; set; }

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

    protected override Dictionary<Type, (int, int)> Drops()
    {
        return new()
        {
            { typeof(CoinModel), (0, 2) },
            { typeof(XpNodeModel), (1, 2) },
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
        MoveTowards(FinalTarget, gameTime);

        if (Point == FinalTarget)
        {
            DelayIdleAtual = DelayIdle;
            MoveStatus = MoveType.Idle;
        }
    }

    private void HandleChase(GameTime gameTime)
    {
        if (GlobalVariables.Player.CenterPosition == Vector2.Zero)
        {
            DelayIdleAtual = DelayIdle;
            MoveStatus = MoveType.Idle;

            return;
        }

        MoveTowards(GlobalVariables.Player.Point, gameTime);
    }

    private void MoveTowards(Point target, GameTime gameTime)
    {
        List<Point> path = FindPath(Point, target);

        if (path.Count > 1)
        {
            Point nextStep = path[1];

            Vector2 targetPos = new Vector2(nextStep.X * WallModel.Size.X, nextStep.Y * WallModel.Size.Y);

            if (nextStep.X == Point.X) targetPos.X = Position.X;
            if (nextStep.Y == Point.Y) targetPos.Y = Position.Y;

            Vector2 direction = targetPos - Position;
            if (direction.LengthSquared() > 0.01f)
            {
                Move(direction, (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }
    }

    private void ChooseNewTarget()
    {
        var walls = GlobalVariables.CurrentRoom.Walls;
        var target = Point.Zero;

        while (target == Point.Zero)
        {
            var tryTarget = new Point(
                new Random().Next(Math.Max(1, Point.X - 5), Math.Min(Point.X + 5, walls.GetLength(0) - 1)),
                new Random().Next(Math.Max(1, Point.Y - 5), Math.Min(Point.Y + 5, walls.GetLength(1) - 1))
                );

            if (walls[tryTarget.X, tryTarget.Y] == null)
            {
                target = tryTarget;
            }
        }

        FinalTarget = target;
        TargetDirection = new Vector2(FinalTarget.X * WallModel.Size.X, FinalTarget.Y * WallModel.Size.Y) - Position;
    }

    public bool CanSeePlayer()
    {
        //return false;

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
