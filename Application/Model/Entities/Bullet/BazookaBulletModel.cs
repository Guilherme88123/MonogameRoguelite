using Application.Model.Entities.Bullet.Base;
using Microsoft.Xna.Framework;
using MonogameRoguelite.Model.Entities.Base;
using System.Collections.Generic;

namespace Application.Model.Entities.Bullet;

public class BazookaBulletModel : BaseBulletModel
{
    private bool hasExplosion = false;
    private float explosionTime = 0.4f;

    public BazookaBulletModel((int x, int y) position, Vector2 direction, BaseEntityModel sender) : base(position, direction, sender)
    {
        Size = new Vector2(24, 24);
        Speed = new Vector2(400, 400) * GunSpeedFactor;
        Color = Color.DarkRed;
        Damage = 8;
    }

    public override void Update(GameTime gameTime, List<BaseEntityModel> entities)
    {
        explosionTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (hasExplosion && explosionTime < 0)
        {
            Destroy();
        }

        base.Update(gameTime, entities);
    }

    protected override void HasDestroyed(GameTime gameTime, List<BaseEntityModel> entities)
    {
        if (!hasExplosion)
        {
            var explosion = new BazookaBulletModel((0, 0), Direction, Sender);
            explosion.hasExplosion = true;
            explosion.Speed = Vector2.Zero;
            explosion.Size *= 8;
            explosion.Position = (Position + Size / 2) - explosion.Size / 2;
            explosion.Color = Color.Red * 0.7f;
            explosion.IsCollidable = false;
            entities.Add(explosion);
        }

        base.HasDestroyed(gameTime, entities);
    }
}
