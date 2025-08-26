using Application.Model.Entities.Collectable.Base;
using Microsoft.Xna.Framework;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities.Base;
using System;
using System.Collections.Generic;

namespace Application.Model.Entities.Collectable.Gun.Base;

public class BaseGunModel : BaseCollectableModel
{
    public BaseEntityModel User { get; set; }

    protected float Delay { get; set; } = 0.35f;
    public float DelayAtual { get; set; }

    public BaseGunModel((float x, float y) position) : base(position)
    {
        Size = new(32, 8);
        Color = Color.Black;
    }

    public void Shoot(List<BaseEntityModel> entities, Vector2 direction)
    {
        if (DelayAtual > 0) return;

        var drops = Bullets();

        foreach (var entityType in drops)
        {
            for (int i = 0; i < entityType.Value; i++)
            {
                var instance = (BaseEntityModel)Activator.CreateInstance(entityType.Key, ((int)User.CenterPosition.X, (int)User.CenterPosition.Y), direction, User)!;
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
        DelayAtual -= (float)gameTime.ElapsedGameTime.TotalSeconds;

        base.Update(gameTime, entities);
    }
}
