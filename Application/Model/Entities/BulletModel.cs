using Microsoft.Xna.Framework;
using System.Collections.Generic;
using MonogameRoguelite.Model.Entities.Base;
using MonogameRoguelite.Model.Entities.Creature.Base;

namespace MonogameRoguelite.Model.Entities;

public class BulletModel : BaseEntityModel
{
    public BaseEntityModel Sender { get; set; }

    public BulletModel((int x, int y) position, Vector2 direction, BaseEntityModel sender) : base(position)
    {
        Size = new Vector2(16, 16);
        Speed = new Vector2(650, 650);
        Color = Color.Yellow;
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
            creature.Health--;
            Destroy();
        }

        if (model is WallModel wall)
        {
            Destroy();
        }

        base.Colision(model);
    }
}
