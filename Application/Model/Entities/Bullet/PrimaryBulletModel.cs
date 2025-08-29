using Application.Model.Entities.Bullet.Base;
using Microsoft.Xna.Framework;
using MonogameRoguelite.Model.Entities.Base;

namespace Application.Model.Entities.Bullet;

public class PrimaryBulletModel : BaseBulletModel
{
    public PrimaryBulletModel((int x, int y) position, Vector2 direction, BaseEntityModel sender, float gunSpeedFactor) : base(position, direction, sender, gunSpeedFactor)
    {
        Size = new Vector2(16, 16);
        Speed = new Vector2(550, 550) * GunSpeedFactor;
        Color = Color.Yellow;
    }
}
