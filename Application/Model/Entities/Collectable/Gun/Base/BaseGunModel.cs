using Application.Infrastructure;
using Application.Model.Entities.Collectable.Base;
using Microsoft.Xna.Framework;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities.Base;
using MonogameRoguelite.Model.Entities.Creature.Base;
using System;
using System.Collections.Generic;

namespace Application.Model.Entities.Collectable.Gun.Base;

public abstract class BaseGunModel : BaseCollectableModel
{
    public BaseCreatureModel User { get; set; }

    protected float Delay { get; set; } = 0.5f;
    public float DelayAtual { get; set; }
    public float BulletSpeedFactor { get; set; } = 1f;

    public BaseGunModel((float x, float y) position) : base(position)
    {
        Size = new(32, 8);
        Color = Color.Black;
        DelayAtual = Delay;
    }

    public void Shoot()
    {
        if (DelayAtual > 0) return;

        var bullets = Bullets();

        foreach (var entityType in bullets)
        {
            if (entityType.Value == 1)
            {
                CreateBullet(entityType.Key);
            }
            else
            {
                for (int i = 0; i < entityType.Value; i++)
                {
                    float spread = MathHelper.ToRadians(10f);

                    float randomAngle = (float)(new Random().NextDouble() * 2 - 1) * spread;

                    Vector2 bulletDir = VectorHelper.Rotate(User.TargetDirection, randomAngle);

                    CreateBullet(entityType.Key, bulletDir);
                }
            }
        }

        DelayAtual = Delay;
    }

    private void CreateBullet(Type type)
    {
        CreateBullet(type, User.TargetDirection);
    }

    private void CreateBullet(Type type, Vector2 direction)
    {
        var instance = (BaseEntityModel)Activator.CreateInstance(type, ((int)User.CenterPosition.X, (int)User.CenterPosition.Y), direction, User, BulletSpeedFactor)!;
        instance.Position -= new Vector2(instance.Size.X / 2, instance.Size.Y / 2);
        GlobalVariables.CurrentRoom.EntitiesToAdd.Add(instance);
    }

    protected virtual Dictionary<Type, int> Bullets()
    {
        return new();
    }

    public override void Update(GameTime gameTime, List<BaseEntityModel> entities)
    {
        var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        DelayAtual -= delta;

        Direction.Normalize();

        base.Update(gameTime, entities);
    }
}
