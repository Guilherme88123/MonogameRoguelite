using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Teste001.Dto;
using Teste001.Model.Entities;
using Teste001.Model.Entities.Base;
using Teste001.Model.Entities.Creature.Enemy.Base;

namespace Application.Model.Entities.Creature.Enemy;

public class FlyModel : BaseEnemyModel
{
    public const float DelayTiro = 1.5f;
    public float DelayTiroAtual { get; set; } = DelayTiro;

    public FlyModel((int x, int y) position) : base(position, 3)
    {
        Size = new Vector2(32, 32);
        Acceleration = 500f;
        Friction = 300f;
        MaxSpeed = 100f;
        Color = Color.DarkGray;
    }

    public override void Update(GameTime gameTime, List<BaseEntityModel> entities)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        DelayTiroAtual -= delta;

        if (DelayTiroAtual <= 0 && MoveStatus == Enum.MoveType.Chase)
        {
            var direction = GlobalVariables.PlayerPosition - Position;
            entities.Add(new BulletModel(((int)(Position.X + Size.X / 2), (int)(Position.Y + Size.Y / 2)), direction, this));
            DelayTiroAtual = DelayTiro;
        }

        base.Update(gameTime, entities);
    }
}
