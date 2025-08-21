using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Teste001.Dto;
using Teste001.Model.Entities.Base;
using Teste001.Model.Entities.Creature.Base;

namespace Teste001.Model.Entities;

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

        if (Position.X <= 0 || Position.X >= GlobalVariables.Graphics.PreferredBackBufferWidth - Size.X && 
            Position.Y <= 0 || Position.Y >= GlobalVariables.Graphics.PreferredBackBufferHeight - Size.Y)
        {
            Destroy();
        }

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
