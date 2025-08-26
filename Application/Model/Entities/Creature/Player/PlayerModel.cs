using Application.Interface.Camera;
using Application.Interface.Room;
using Application.Model.Entities.Collectable.Base;
using Application.Model.Entities.Collectable.Gun;
using Application.Model.Entities.Collectable.Gun.Base;
using Application.Model.Entities.Collectable.Item.Base;
using Application.Model.Entities.Drop.Heart;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities.Base;
using MonogameRoguelite.Model.Entities.Creature.Base;
using MonogameRoguelite.Model.Entities.Creature.Enemy.Base;
using MonogameRoguelite.Model.Entities.Drop.Base;
using MonogameRoguelite.Model.Entities.Drop.Coin;
using MonogameRoguelite.Model.Entities.Drop.Xp;
using System.Collections.Generic;

namespace MonogameRoguelite.Model.Entities.Creature.Player;

public class PlayerModel : BaseCreatureModel
{
    public int Coins { get; set; }
    public int MaxXp { get; set; }
    public int Xp { get; set; }
    public int Level { get; set; }

    public const float DelayDano = 1f;
    public float DelayDanoAtual { get; set; }

    public int MaxGuns = 3;
    public List<BaseGunModel> Guns { get; set; } = new();
    public BaseGunModel EquippedGun { get; set; } = new((0, 0));

    public List<BaseItemModel> Inventory { get; set; } = new();

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

        Guns.Add(new PrimaryGunModel((0, 0)));
        EquippedGun = Guns[0];
        EquippedGun.User = this;
    }

    public override void Update(GameTime gameTime, List<BaseEntityModel> entities)
    {
        var teclado = Keyboard.GetState();
        var mouse = Mouse.GetState();

        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        EquippedGun.Update(gameTime, entities);

        #region Delays

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

        var camera = GlobalVariables.GetService<ICameraService>();
        var mouseCamera = Vector2.Transform(new Vector2(mouse.X, mouse.Y), Matrix.Invert(camera.Transform));
        TargetDirection = mouseCamera - Position;

        if (mouse.LeftButton == ButtonState.Pressed)
        {
            EquippedGun.Shoot(entities);
        }

        #endregion

        base.Update(gameTime, entities);

        GlobalVariables.Player.Position = new Vector2(Position.X, Position.Y);
    }

    public override void Colision(BaseEntityModel model)
    {
        if (model is BaseCollectableModel colec)
        {
            if (colec is BaseGunModel gun && Guns.Count < MaxGuns)
            {
                Guns.Add(gun);
            }
            else if (colec is BaseItemModel item)
            {
                Inventory.Add(item);
            }

            colec.Destroy();
        }

        if (model is BaseEnemyModel enemy && DelayDanoAtual <= 0)
        {
            Health--;
            DelayDanoAtual = DelayDano;
            if (Health <= 0) Destroy();
        }

        if (model is BaseDropModel drop)
        {
            if (model is CoinModel coin)
            {
                Coins += coin.Value;
            }

            if (model is HeartModel hearth)
            {
                if (Health < MaxHealth)
                    Health += hearth.Value;
            }

            if (model is XpNodeModel xp)
            {
                Xp += xp.Value;

                if (Xp >= MaxXp)
                {
                    Level++;
                    Xp -= MaxXp;
                    MaxXp = (int)(MaxXp * 1.5);
                }
            }

            drop.Destroy();
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
        DrawGun();

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

    private void DrawGun()
    {
        var rotation = (float)System.Math.Atan2(TargetDirection.Y, TargetDirection.X);
       
        GlobalVariables.SpriteBatchEntities.Draw(
            GlobalVariables.Pixel,
            CenterPosition,
            null,
            EquippedGun.Color,
            rotation,                            
            new Vector2(0.5f, 0.5f),                 
            EquippedGun.Size      ,       
            SpriteEffects.None,
            0f);
    }
}
