using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Teste001.Dto;
using Teste001.Interface;
using Teste001.Model.Entities;
using Teste001.Model.Entities.Base;
using Teste001.Model.Entities.Creature.Enemy.Base;
using Teste001.Model.Entities.Creature.Player;

namespace Teste001.Model.Room.Base;

public abstract class BaseRoomModel
{
    public List<BaseEntityModel> Entities = new();
    public List<BaseEntityModel> EntitiesToAdd = new();

    public int Width { get; }
    public int Height { get; }

    public bool Finished { get; set; } = false;
    public bool Visited { get; set; } = false;

    protected BaseRoomModel(int width, int height)
    {
        Width = width;
        Height = height;
    }

    protected (int, int) GetRandomPosition()
    {
        var random = new Random();
        var x = random.Next(50, Width - 50);
        var y = random.Next(50, Height - 50);

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
        Entities.ForEach(x => x.Draw());
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

        //var wall = new WallModel(GetRandomPosition());
        //EntitiesToAdd.Add(wall);
    }
}
