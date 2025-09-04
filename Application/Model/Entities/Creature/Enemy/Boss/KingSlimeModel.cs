using Application.Enum;
using Application.Model.Entities.Bullet;
using Application.Model.Entities.Drop.Heart;
using Microsoft.Xna.Framework;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities.Base;
using MonogameRoguelite.Model.Entities.Creature.Enemy.Base;
using MonogameRoguelite.Model.Entities.Drop.Coin;
using MonogameRoguelite.Model.Entities.Drop.Xp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonogameRoguelite.Model.Entities.Creature.Enemy.Boss;

public class KingSlimeModel : BaseEnemyModel
{
    public float DelaySpecialAttack = 10f;
    public float DelaySpecialAttackAtual { get; set; }

    public bool PlayerMarcado { get; set; } = false;
    public Rectangle PlayerPostion { get; set; }

    public bool FirstAttack { get; set; } = false;

    public KingSlimeModel((float x, float y) position) : base(position, 100)
    {
        Size = new Vector2(128, 128);
        Acceleration = 600f;
        Friction = 320f;
        MaxSpeed = 100f;
        Color = Color.DarkGreen;
        Name = "King Slime";
        MoveStatus = MoveType.Chase;
        DelaySpecialAttackAtual = 5f;
    }

    public override void Update(GameTime gameTime, List<BaseEntityModel> entities)
    {
        base.Update(gameTime, entities);

        DelaySpecialAttackAtual -= (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (DelaySpecialAttackAtual <= 0f)
        {
            SpecialAttack();

            DelaySpecialAttackAtual = DelaySpecialAttack;
        }
    }

    private void SpecialAttack()
    {
        if (!FirstAttack)
        {
            JumpOnPlayer();
            FirstAttack = true;
        }

        var attk = new Random().Next(0, 3);

        if (attk == 0) SpawnMinions();
        else if (attk == 1) JumpOnPlayer();
        else ShootProjectiles();
    }

    private async Task SpawnMinions()
    {
        (int, int) position = new((int)CenterPosition.X, (int)CenterPosition.Y);

        GlobalVariables.CurrentRoom.EntitiesToAdd.Add(new SlimeModel(position));
        await Task.Delay(500);
        GlobalVariables.CurrentRoom.EntitiesToAdd.Add(new SlimeModel(position));
    }

    private async Task JumpOnPlayer()
    {
        PlayerPostion = new((int)(GlobalVariables.Player.CenterPosition.X - Size.X / 2),
                            (int)(GlobalVariables.Player.CenterPosition.Y - Size.Y / 2),
                            (int)Size.X,
                            (int)Size.Y);

        PlayerMarcado = true;

        await Task.Delay(1200);

        PlayerMarcado = false;
        Position = new Vector2(PlayerPostion.X, PlayerPostion.Y);
    }

    private async Task ShootProjectiles()
    {
        (int, int) position = new((int)CenterPosition.X, (int)CenterPosition.Y);

        for (var i = 0; i < 8; i++)
        {
            if (i % 2 == 0) ShootBullets(12);
            else ShootBullets(12, MathF.PI / 12);

            await Task.Delay(250);
        }
    }

    private void ShootBullets(int quantity, float offset = 0)
    {
        (int, int) position = new((int)CenterPosition.X, (int)CenterPosition.Y);

        for (int i = 0; i < quantity; i++)
        {
            float angle = (MathF.PI * 2f * i / quantity) + offset; // de 0 a 2PI
            Vector2 direction = new Vector2(MathF.Cos(angle), MathF.Sin(angle));

            GlobalVariables.CurrentRoom.EntitiesToAdd.Add(new EnemyBulletModel(position, direction, this, 0.7f));
        }
    }

    protected override Dictionary<Type, (int, int)> Drops()
    {
        return new()
        {
            { typeof(HeartModel), (0, 2) },
            { typeof(CoinModel), (1, 3) },
            { typeof(XpNodeModel), (2, 3) },
        };
    }

    public override void Draw()
    {
        base.Draw();

        var x = GlobalVariables.Graphics.PreferredBackBufferWidth / 4;
        var width = x * 2;
        var y = 20;
        var height = 30;

        GlobalVariables.SpriteBatchInterface.Draw(GlobalVariables.Pixel, new Rectangle(x, y, width, height), Color.DarkGray);

        var healthWidth = (int)((Health / (float)MaxHealth) * (width - 10));
        var healthX = x + 5;
        var healthY = y + 5;
        var healthHeight = height - 10;
        GlobalVariables.SpriteBatchInterface.Draw(GlobalVariables.Pixel, new Rectangle(healthX, healthY, healthWidth, healthHeight), Color.Red);

        var textName = Name;

        var textNameSize = GlobalVariables.Font.MeasureString(textName);

        GlobalVariables.SpriteBatchInterface.DrawString(GlobalVariables.Font, textName, new Vector2(healthX + 5, healthY + healthHeight / 2 - textNameSize.Y / 2), Color.White);

        if (PlayerMarcado)
        {
            GlobalVariables.SpriteBatchEntities.Draw(GlobalVariables.Pixel, PlayerPostion, Color.Red * 0.5f);
        }
    }
}
