using Application.Enum;
using Application.Model.Entities.Collectable.Gun.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities.Base;
using MonogameRoguelite.Model.Entities.Creature.Enemy.Base;
using System;
using System.Collections.Generic;

namespace Application.Model.Entities.Creature.Enemy.Ranged.Base;

public class BaseRangedEnemyModel : BaseEnemyModel
{
    public BaseGunModel Gun { get; set; }

    public BaseRangedEnemyModel((float x, float y) position, int maxHealt) : base(position, maxHealt)
    {
    }

    public override void Update(GameTime gameTime, List<BaseEntityModel> entities)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        Gun.Update(gameTime, entities);

        if (MoveStatus == MoveType.Chase && CanSeePlayer())
        {
            TargetDirection = Vector2.Normalize(GlobalVariables.Player.CenterPosition - CenterPosition);
            Gun.Shoot();
        }

        base.Update(gameTime, entities);
    }

    public override void Draw()
    {
        base.Draw();
        DrawGun();
    }

    private void DrawGun()
    {
        var rotation = (float)Math.Atan2(TargetDirection.Y, TargetDirection.X);

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
