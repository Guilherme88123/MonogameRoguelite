using Application.Interface.Room;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities.Base;
using MonogameRoguelite.Model.Entities.Creature.Base;
using MonogameRoguelite.Model.Entities.Creature.Enemy.Base;
using MonogameRoguelite.Model.Entities.Drop.Coin;
using MonogameRoguelite.Model.Entities.Drop.Xp;
using Application.Interface.Camera;

namespace MonogameRoguelite.Model.Entities.Creature.Player;

public class PlayerModel : BaseCreatureModel
{
    public int Coins { get; set; }
    public int MaxXp { get; set; }
    public int Xp { get; set; }
    public int Level { get; set; }

    public const float DelayTiro = 0.35f;
    public float DelayTiroAtual { get; set; }

    public const float DelayDano = 1f;
    public float DelayDanoAtual { get; set; }

    public PlayerModel((int x, int y) position) : base(position, maxHealth: 5)
    {
        Acceleration = 1500f;
        Friction = 800f;
        MaxSpeed = 300f;
        Color = Color.Blue;
        Direction = new Vector2(1, 1);
        MaxXp = 5;
        Level = 1;
        Size = new Vector2(48, 64);
    }

    public override void Update(GameTime gameTime, List<BaseEntityModel> entities)
    {
        var teclado = Keyboard.GetState();
        var mouse = Mouse.GetState();

        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        #region Delays

        DelayTiroAtual -= delta;
        DelayDanoAtual -= delta;

        #endregion 

        #region Movimentação

        var direcX = 0f;
        var direcY = 0f;

        if (teclado.IsKeyDown(Keys.Right) || teclado.IsKeyDown(Keys.D))
        {
            direcX += 1;
        }
        else if (teclado.IsKeyDown(Keys.Left) || teclado.IsKeyDown(Keys.A))
        {
            direcX = -1;
        }

        if (teclado.IsKeyDown(Keys.Down) || teclado.IsKeyDown(Keys.S))
        {
            direcY += 1;
        }
        else if (teclado.IsKeyDown(Keys.Up) || teclado.IsKeyDown(Keys.W))
        {
            direcY += -1;
        }

        Move(new Vector2(direcX, direcY), delta);

        #endregion

        #region Mouse Inputs

        if (mouse.LeftButton == ButtonState.Pressed && DelayTiroAtual <= 0)
        {
            var camera = GlobalVariables.GetService<ICameraService>();
            var mouseCamera = Vector2.Transform(new Vector2(mouse.X, mouse.Y), Matrix.Invert(camera.Transform));
            var direction = mouseCamera - Position;
            entities.Add(new BulletModel(((int)(Position.X + Size.X / 2), (int)(Position.Y + Size.Y / 2)), direction, this));
            DelayTiroAtual = DelayTiro;
        }

        #endregion

        base.Update(gameTime, entities);

        GlobalVariables.PlayerPosition = new Vector2(Position.X, Position.Y);
    }

    public override void Colision(BaseEntityModel model)
    {
        if (model is BaseEnemyModel enemy && DelayDanoAtual <= 0)
        {
            Health--;
            DelayDanoAtual = DelayDano;
            if (Health <= 0) Destroy();
        }

        if (model is CoinModel coin)
        {
            coin.Destroy();
            Coins += coin.Value;
        }

        if (model is XpNodeModel xp)
        {
            xp.Destroy();
            Xp += xp.Value;

            if (Xp >= MaxXp)
            {
                Level++;
                Xp -= MaxXp;
                MaxXp = (int)(MaxXp * 1.5);
            }
        }

        if (model is DoorModel door)
        {
            var mapService = GlobalVariables.GetService<IMapService>();

            if (!GlobalVariables.CurrentRoom.Finished) return;

            mapService.Move(door.DirectionPosition, this);
        }

        base.Colision(model);
    }

    public override void Draw()
    {
        base.Draw();

        DrawBar(0, MaxHealth, Health, Color.Red);
        DrawBar(1, MaxXp, Xp, Color.Purple);

        GlobalVariables.SpriteBatchInterface.DrawString(GlobalVariables.Font, $"Level: {Level}", new Vector2(10, 62), Color.White);

        GlobalVariables.SpriteBatchInterface.DrawString(GlobalVariables.Font, $"Coins: {Coins}", new Vector2(10, 86), Color.White);
    }

    private void DrawBar(int count, int max, int current, Color color)
    {
        var width = 16 * max + 8;
        var height = 24;
        var x = 10;
        var y = 10 + height * count + 4 * count;

        GlobalVariables.SpriteBatchInterface.Draw(GlobalVariables.Pixel, new Rectangle(x, y, width, height), Color.DarkGray);

        for (var i = 0; i < current; i++)
        {
            GlobalVariables.SpriteBatchInterface.Draw(GlobalVariables.Pixel, new Rectangle(10 + 4 + i * 16, y + 4, 16, 16), color);
        }

        GlobalVariables.SpriteBatchInterface.DrawString(GlobalVariables.Font, $"{current}/{max}", new Vector2(width / 2, y + 4), Color.White);
    }
}
