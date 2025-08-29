using Application.Model.Entities.Bullet.Base;
using Microsoft.Xna.Framework;
using MonogameRoguelite.Model.Entities.Base;

namespace Application.Model.Entities.Bullet;

public class EnemyBulletModel : BaseBulletModel
{
    public EnemyBulletModel((int x, int y) position, Vector2 direction, BaseEntityModel sender) : base(position, direction, sender)
    {
        Size = new Vector2(16, 16);
        Speed = new Vector2(300, 300) * GunSpeedFactor;
        Color = Color.IndianRed;
        
    }
}
