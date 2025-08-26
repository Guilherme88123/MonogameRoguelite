using Microsoft.Xna.Framework;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities;
using MonogameRoguelite.Model.Entities.Base;
using MonogameRoguelite.Model.Entities.Creature.Enemy.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using static System.Net.Mime.MediaTypeNames;

namespace MonogameRoguelite.Model.Room.Base;

public abstract class BaseRoomModel
{
    public Vector2 NextRoomPosition { get; set; } = Vector2.Zero;

    public List<BaseEntityModel> Entities = new();
    public List<BaseEntityModel> EntitiesToAdd = new();

    public Vector2 Size { get; }

    public bool Finished { get; set; } = false;
    public bool Visited { get; set; } = false;

    public float DelayAfterFinish = 2.0f;

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
        var tileSize = WallModel.DefaultSize;

        EntitiesToAdd.Add(new WallModel((-tileSize, -tileSize), (Size.X + 2 * tileSize, tileSize)));
        EntitiesToAdd.Add(new WallModel((-tileSize, Size.Y), (Size.X + 2 * tileSize, tileSize)));

        EntitiesToAdd.Add(new WallModel((-tileSize, 0), (tileSize, Size.Y)));
        EntitiesToAdd.Add(new WallModel((Size.X, 0), (tileSize, Size.Y)));

        AddObstacles();
    }

    protected virtual void AddObstacles()
    {
    }

    protected virtual void HasFinished()
    {
    }
}
