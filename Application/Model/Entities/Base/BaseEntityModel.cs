using Application.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameRoguelite.Dto;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MonogameRoguelite.Model.Entities.Base;

public abstract class BaseEntityModel
{
    public Vector2 Size { get; set; } = new Vector2(64, 64);
    public Vector2 Speed { get; set; } = new();
    public Color Color { get; set; } = Color.White;
    public Vector2 Position { get; set; } = new();
    public Vector2 CenterPosition => new Vector2(Position.X + Size.X / 2, Position.Y + Size.Y / 2);
    public Vector2 Direction { get; set; } = new();
    public float Acceleration { get; set; }
    public float Friction { get; set; }
    public float MaxSpeed { get; set; }
    public bool IsCollidable { get; set; } = true;
    public string Name { get; set; }
    public Point Point => new Point((int)CenterPosition.X / (int)WallModel.Size.X, (int)CenterPosition.Y / (int)WallModel.Size.Y);

    public bool IsDestroyed { get; set; } = false;

    public Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

    protected BaseEntityModel((float x, float y) position)
    {
        Position = new Vector2(position.x, position.y);
    }

    public virtual void Update(GameTime gameTime, List<BaseEntityModel> entities)
    {
        if (IsDestroyed)
        {
            HasDestroyed(gameTime, entities);
        }
    }

    public virtual void Colision(BaseEntityModel model)
    {
    }

    public virtual void WallColision(WallModel model)
    {
        var posX = Position.X;
        var posY = Position.Y;

        var intersection = Rectangle.Intersect(Rectangle, model.Rectangle);

        if (intersection.Width < intersection.Height)
        {
            // Corrige X
            if (Position.X < model.Position.X)
                posX -= intersection.Width;
            else
                posX += intersection.Width;
        }
        else
        {
            // Corrige Y
            if (Position.Y < model.Position.Y)
                posY -= intersection.Height;
            else
                posY += intersection.Height;
        }

        Position = new Vector2(posX, posY);
    }

    public virtual void Draw()
    {
        GlobalVariables.SpriteBatchEntities.Draw(GlobalVariables.Pixel, Rectangle, Color);
    }

    public virtual void Destroy()
    {
        IsDestroyed = true;
    }

    protected virtual void HasDestroyed(GameTime gameTime, List<BaseEntityModel> entities)
    {
        var drops = Drops();

        foreach (var entityType in drops)
        {
            var quantidadeDrops = new Random().Next(entityType.Value.Item1, entityType.Value.Item2 + 1);

            for (int i = 0; i < quantidadeDrops; i++)
            {
                var instance = (BaseEntityModel)Activator.CreateInstance(entityType.Key, ((int)Position.X, (int)Position.Y))!;
                entities.Add(instance);
            }
        }
    }

    protected virtual Dictionary<Type, (int, int)> Drops()
    {
        return new();
    }

    protected void Move(Vector2 direction, float delta)
    {
        if (direction != Vector2.Zero)
        {
            direction.Normalize();
            Speed += direction * Acceleration * delta;
            Direction = direction;
        }
        else
        {
            // Aplica atrito
            if (Speed.Length() > 0)
            {
                Vector2 frictionVector = Vector2.Normalize(Speed) * Friction * delta;
                if (frictionVector.Length() > Speed.Length())
                    Speed = Vector2.Zero;
                else
                    Speed -= frictionVector;
            }
        }

        if (Speed.Length() > MaxSpeed)
            Speed = Vector2.Normalize(Speed) * MaxSpeed;

        Position += Speed * delta;
    }
}
