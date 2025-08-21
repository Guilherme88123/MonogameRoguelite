using Application.Enum;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Teste001.Dto;
using Teste001.Model.Entities.Base;

namespace Teste001.Model.Entities.Creature.Base;

public abstract class BaseCreatureModel : BaseEntityModel
{
    public int MaxHealth { get; set; }
    public int Health { get; set; }
    public MoveType MoveStatus { get; set; } = MoveType.Idle;

    public BaseCreatureModel((int x, int y) position, int maxHealth) : base(position)
    {
        MaxHealth = maxHealth;
        Health = MaxHealth;
    }

    public override void Update(GameTime gameTime, List<BaseEntityModel> entities)
    {
        if (Health <= 0)
        {
            Destroy();
        }

        var posX = Position.X;
        var posY = Position.Y;

        #region Colisão com bordas da tela

        posX = MathHelper.Clamp(Position.X, 0, GlobalVariables.Graphics.PreferredBackBufferWidth - Size.X);
        posY = MathHelper.Clamp(Position.Y, 0, GlobalVariables.Graphics.PreferredBackBufferHeight - Size.Y);

        #endregion

        Position = new Vector2(posX, posY);

        base.Update(gameTime, entities);
    }
}
