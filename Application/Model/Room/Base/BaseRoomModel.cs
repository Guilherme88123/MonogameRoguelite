using Application.Model;
using Microsoft.Xna.Framework;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities.Base;
using MonogameRoguelite.Model.Entities.Creature.Enemy.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.Marshalling;
using static System.Net.Mime.MediaTypeNames;

namespace MonogameRoguelite.Model.Room.Base;

public abstract class BaseRoomModel
{
    public Vector2 NextRoomPosition { get; set; } = Vector2.Zero;

    public List<BaseEntityModel> Entities = new();
    public List<BaseEntityModel> EntitiesToAdd = new();
    public WallModel[,] Walls;

    public Vector2 Size { get; }

    public bool Finished { get; set; } = false;
    public bool Visited { get; set; } = false;
    public bool Loaded { get; set; } = false;

    public float DelayAfterFinish { get; set; } = 2.0f;

    protected BaseRoomModel(int width, int height)
    {
        Size = new Vector2(width * WallModel.Size.X, height * WallModel.Size.Y);

        Walls = new WallModel[width, height];

        LoadWalls();
    }

    protected (float, float) GetRandomPosition()
    {
        var walls = GlobalVariables.CurrentRoom.Walls;
        var target = Point.Zero;

        var playerAreaRectangle = new Rectangle(
            (GlobalVariables.Player.Point.X - 5),
            (GlobalVariables.Player.Point.Y - 5),
            10,
            10);

        while (target == Point.Zero)
        {
            var tryTarget = new Point(
                new Random().Next(1, walls.GetLength(0) - 1),
                new Random().Next(1, walls.GetLength(1) - 1)
                );

            if (walls[tryTarget.X, tryTarget.Y] == null && !playerAreaRectangle.Contains(tryTarget))
            {
                target = tryTarget;
            }
        }

        return (target.X * WallModel.Size.X, target.Y * WallModel.Size.Y);
    }

    public virtual void Update(GameTime gameTime)
    {
        if (EntitiesToAdd.Any())
        {
            Entities.AddRange(EntitiesToAdd);
            EntitiesToAdd.Clear();
        }

        VerifyEnemies();
        VerifyCollision();
        VerifyDelays(gameTime);

        Entities.ForEach(entity => entity.Update(gameTime, EntitiesToAdd));
        Entities.RemoveAll(x => x.IsDestroyed);
    }

    public virtual void Draw()
    {
        DrawGround();

        Entities.ForEach(x => x.Draw());

        DrawWalls();

        if (Finished && DelayAfterFinish > 0.1f)
        {
            DrawFinishedMessage();
        }
    }

    private void DrawGround()
    {
        var ground = new Rectangle(0, 0, (int)Size.X, (int)Size.Y);

        GlobalVariables.SpriteBatchEntities.Draw(GlobalVariables.Pixel, ground, Color.MediumSeaGreen);
    }

    private void DrawFinishedMessage()
    {
        var Text = "Room Cleared!";
        var textSize = GlobalVariables.Font.MeasureString(Text);
        var textPosition = new Vector2(
            (GlobalVariables.Graphics.PreferredBackBufferWidth - textSize.X) / 2,
            (GlobalVariables.Graphics.PreferredBackBufferHeight - textSize.Y) / 2);
        GlobalVariables.SpriteBatchInterface.DrawString(GlobalVariables.Font, Text, textPosition, Color.White);
    }

    private void DrawWalls()
    {
        for (int x = 0; x < Walls.GetLength(0); x++)
        {
            for (int y = 0; y < Walls.GetLength(1); y++)
            {
                var wall = Walls[x, y];
                if (wall != null) wall.Draw(x, y);
            }
        }
    }

    protected virtual void VerifyDelays(GameTime gameTime)
    {
        if (Finished && DelayAfterFinish > 0)
            DelayAfterFinish -= (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    private void VerifyEnemies()
    {
        bool hasEnemies = Entities.Any(x => x is BaseEnemyModel);

        if (!hasEnemies && !Finished)
        {
            Finished = true;
            HasFinished();
        }
    }

    private void VerifyCollision()
    {
        for (int i = 0; i < Entities.Count; i++)
        {
            foreach (var wall in Walls)
            {
                if (wall != null && Entities[i].Rectangle.Intersects(wall.Rectangle) && Entities[i].IsCollidable)
                    Entities[i].WallColision(wall);
            }

            for (int j = i + 1; j < Entities.Count; j++)
            {
                var entity = Entities[i];
                var otherEntity = Entities[j];

                if (entity.Rectangle.Intersects(otherEntity.Rectangle))
                {
                    entity.Colision(otherEntity);
                    otherEntity.Colision(entity);
                }
            }
        }
    }

    public void OnRoomEnter()
    {
        if (Loaded) return;

        LoadInitialEntities();

        Loaded = true;
    }

    protected virtual Dictionary<int, Type> InitialEntities()
    {
        return new();
    }

    private void LoadInitialEntities()
    {
        var entities = InitialEntities();

        if (entities == null || !entities.Any()) return;

        foreach (var entityType in entities)
        {
            for (int i = 0; i < entityType.Key; i++)
            {
                var instance = (BaseEntityModel)Activator.CreateInstance(entityType.Value, GetRandomPosition())!;
                EntitiesToAdd.Add(instance);
            }
        }
    }

    private void LoadWalls()
    {
        for (int x = 0; x < Walls.GetLength(0); x++)
        {
            for (int y = 0; y < Walls.GetLength(1); y++)
            {
                bool isBorder = (x == 0 || y == 0 ||
                                 x == Walls.GetLength(0) - 1 ||
                                 y == Walls.GetLength(1) - 1);

                if (isBorder)
                {
                    Walls[x, y] = new WallModel();
                }
            }
        }

        var obstacles = AddObstacles();

        if (obstacles == null) return;

        RegisterObstacles(obstacles);
    }

    protected virtual string[] AddObstacles()
    {
        return null;
    }

    protected virtual void HasFinished()
    {
    }

    private void RegisterObstacles(string[] obstacles)
    {
        var interiorWidth = Walls.GetLength(0);
        var interiorHeight = Walls.GetLength(1);

        if (obstacles.Length != interiorHeight ||
            obstacles[0].Length != interiorWidth)
        {
            throw new Exception("Obstacles layout size is invalid.");
        }

        for (int y = 0; y < interiorHeight; y++)
        {
            for (int x = 0; x < interiorWidth; x++)
            {
                char c = obstacles[y][x];

                if (c == 'X')
                {
                    Walls[x, y] = new WallModel();
                }
            }
        }
    }
}
