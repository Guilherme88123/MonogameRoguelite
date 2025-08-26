using Application.Model.Entities.Bullet;
using Application.Model.Entities.Collectable.Gun;
using Application.Model.Entities.Collectable.Gun.Base;
using Application.Model.Entities.Drop.Heart;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities;
using MonogameRoguelite.Model.Entities.Base;
using MonogameRoguelite.Model.Entities.Creature.Enemy.Base;
using MonogameRoguelite.Model.Entities.Drop.Coin;
using MonogameRoguelite.Model.Entities.Drop.Xp;
using System;
using System.Collections.Generic;

namespace Application.Model.Entities.Creature.Enemy;

public class FlyModel : BaseEnemyModel
{
    public BaseGunModel Gun { get; set; }

    public FlyModel((int x, int y) position) : base(position, 3)
    {
        Size = new Vector2(32, 32);
        Acceleration = 500f;
        Friction = 300f;
        MaxSpeed = 100f;
        Color = Color.DarkGray;
        VisionRange = 500f;

        Gun = new EnemyGunModel((0, 0));
        Gun.User = this;
    }

    public override void Update(GameTime gameTime, List<BaseEntityModel> entities)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        Gun.Update(gameTime, entities);

        if (MoveStatus == Enum.MoveType.Chase)
        {
            Gun.Shoot(entities);
        }

        base.Update(gameTime, entities);
    }

    protected override Dictionary<Type, int> Drops()
    {
        return new()
        {
            { typeof(HeartModel), 1 },
            { typeof(XpNodeModel), 1 },
        };
    }

    public override void Draw()
    {
        base.Draw();
        DrawGun();
    }
    private void DrawGun()
    {
        var rotation = (float)System.Math.Atan2(TargetDirection.Y, TargetDirection.X);

        GlobalVariables.SpriteBatchEntities.Draw(
            GlobalVariables.Pixel,
            CenterPosition,
            null,
            Gun.Color,
            rotation,
            new Vector2(0.5f, 0.5f),
            Gun.Size,
            SpriteEffects.None,
            0f);
    }
}
