using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Teste001.Dto;
using Teste001.Model.Entities.Base;
using Teste001.Model.Entities.Creature.Enemy.Base;

namespace Teste001.Model.Entities;

public class BulletModel : BaseEntityModel
{
    public BulletModel((int x, int y) position, Vector2 direction) : base(position)
    {
        Size = 16;
        Speed = new Vector2(650, 650);
        Color = Color.Yellow;

        direction.Normalize();
        Direction = direction;
    }

    public override void Update(GameTime gameTime, List<BaseEntityModel> entities)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        Position += Speed * Direction * delta;

        if (Position.X <= 0 || Position.X >= GlobalVariables.Graphics.PreferredBackBufferWidth - Size && 
            Position.Y <= 0 || Position.Y >= GlobalVariables.Graphics.PreferredBackBufferHeight - Size)
        {
            Destroy();
        }

        base.Update(gameTime, entities);
    }

    public override void Colision(BaseEntityModel model)
    {
        if (model is BaseEnemyModel creature)
        {
            creature.Health--;
            if (creature.Health <= 0) creature.Destroy();
            Destroy();
        }

        base.Colision(model);
    }
}
