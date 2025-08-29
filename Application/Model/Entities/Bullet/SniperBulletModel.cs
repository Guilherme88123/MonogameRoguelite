using Application.Model.Entities.Bullet.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities.Base;
using System;

namespace Application.Model.Entities.Bullet;

public class SniperBulletModel : BaseBulletModel
{
    public SniperBulletModel((int x, int y) position, Vector2 direction, BaseEntityModel sender) : base(position, direction, sender)
    {
        Size = new Vector2(20, 20);
        Color = Color.DarkOrange;
        Speed = new Vector2(1000, 1000) * GunSpeedFactor;
        Damage = 5;
    }
}
