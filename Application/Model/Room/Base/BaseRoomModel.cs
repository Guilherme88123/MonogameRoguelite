using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities.Base;
using MonogameRoguelite.Model.Entities.Creature.Enemy.Base;
using MonogameRoguelite.Model.Entities;

namespace MonogameRoguelite.Model.Room.Base;

public abstract class BaseRoomModel
{
    public Vector2 NextRoomPosition { get; set; } = Vector2.Zero;

    public List<BaseEntityModel> Entities = new();
    public List<BaseEntityModel> EntitiesToAdd = new();

    public Vector2 Size { get; }

    public bool Finished { get; set; } = false;
    public bool Visited { get; set; } = false;

    protected BaseRoomModel(int width, int height)
    {
        Size = new Vector2(width, height);

        LoadWalls();
    }

    protected (int, int) GetRandomPosition()
    {
        var random = new Random();
        var x = random.Next(50, (int)Size.X - 50);
        var y = random.Next(50, (int)Size.Y - 50);

        return (x, y);
    }

    public virtual void Update(GameTime gameTime)
    {
        if (EntitiesToAdd.Count > 0)
        {
            Entities.AddRange(EntitiesToAdd);
            EntitiesToAdd.Clear();
        }

        VerifyEnemies();
        VerifyCollision();

        Entities.ForEach(entity => entity.Update(gameTime, EntitiesToAdd));
        Entities.RemoveAll(x => x.IsDestroyed);
    }

    public virtual void Draw()
    {
        DrawGround();

        Entities.ForEach(x => x.Draw());
    }

    private void DrawGround()
    {
        var ground = new Rectangle(0, 0, (int)Size.X, (int)Size.Y);

        GlobalVariables.SpriteBatchEntities.Draw(GlobalVariables.Pixel, ground, Color.MediumSeaGreen);
    }

    private void VerifyEnemies()
    {
        bool hasEnemies = Entities.Any(x => x is BaseEnemyModel);

        if (!hasEnemies) Finished = true;
    }

    private void VerifyCollision()
    {
        for (int i = 0; i < Entities.Count; i++)
        {
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

    protected void LoadInitialEntities(Dictionary<int, Type> entities)
    {
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
        var tileSize = (int)new WallModel((0, 0)).Size.X;

        for (int x = 0; x < Size.X; x += tileSize)
        {
            EntitiesToAdd.Add(new WallModel((x, -tileSize)));
            EntitiesToAdd.Add(new WallModel((x, (int)Size.Y)));
        }

        for (int y = 0; y < Size.Y; y += tileSize)
        {
            EntitiesToAdd.Add(new WallModel((-tileSize, y)));
            EntitiesToAdd.Add(new WallModel(((int)Size.X, y)));
        }

        AddObstacles();
    }

    protected virtual void AddObstacles()
    {

    }
}
