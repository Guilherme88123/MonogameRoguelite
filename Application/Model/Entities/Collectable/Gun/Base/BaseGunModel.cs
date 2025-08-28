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

    public BaseGunModel((float x, float y) position) : base(position)
    {
        Size = new(32, 8);
        Color = Color.Black;
    }

    public void Shoot(List<BaseEntityModel> entities)
    {
        if (DelayAtual > 0) return;

        var drops = Bullets();

        foreach (var entityType in drops)
        {
            for (int i = 0; i < entityType.Value; i++)
            {
                var instance = (BaseEntityModel)Activator.CreateInstance(entityType.Key, ((int)User.CenterPosition.X, (int)User.CenterPosition.Y), User.TargetDirection, User)!;
                instance.Position -= new Vector2(instance.Size.X / 2, instance.Size.Y / 2);
                entities.Add(instance);
            }
        }

        DelayAtual = Delay;
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
