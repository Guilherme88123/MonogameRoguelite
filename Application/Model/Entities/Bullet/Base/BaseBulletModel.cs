using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities.Base;
using MonogameRoguelite.Model.Entities.Creature.Base;
using System.Collections.Generic;

namespace Application.Model.Entities.Bullet.Base;

public class BaseBulletModel : BaseEntityModel
{
    public BaseEntityModel Sender { get; set; }
    public int Damage { get; set; } = 1;
    //public float GunDamageFactor { get; set; } = 1f;
    public float GunSpeedFactor { get; set; } = 1f;
    public int QuantityRicochets { get; set; } = 0;
    public int MaxQuantityRicochets { get; set; } = 3;

    public BaseBulletModel((int x, int y) position, Vector2 direction, BaseEntityModel sender, float gunSpeedFactor = 1f) : base(position)
    {
        Sender = sender;

        direction.Normalize();
        Direction = direction;
    }

    public override void Update(GameTime gameTime, List<BaseEntityModel> entities)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        Position += Speed * Direction * delta;

        base.Update(gameTime, entities);
    }

    public override void Colision(BaseEntityModel model)
    {
        if (model is BaseCreatureModel creature && creature != Sender)
        {
            creature.Health -= Damage;
            Destroy();
        }

        base.Colision(model);
    }

    public override void WallColision(WallModel model)
    {
        if (GlobalVariables.Player.HasRicochetBullets && QuantityRicochets < MaxQuantityRicochets)
        {
            var rect = Rectangle;
            var wallRect = model.Rectangle;

            Rectangle.Intersect(ref rect, ref wallRect, out var intersection);

            if (intersection.Width < intersection.Height)
            {
                Direction = new Vector2(-Direction.X, Direction.Y);
            }
            else
            {
                Direction = new Vector2(Direction.X, -Direction.Y);
            }

            QuantityRicochets += 1;
        }
        else
        {
            Destroy();
        }
    }
}
