using Application.Infrastructure;
using Application.Interface.Camera;
using Application.Interface.Room;
using Application.Model.Entities;
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

    public const float DelayInv = 0.3f;
    public float DelayInvAtual { get; set; }

    public const float DelayChangeGun = 0.3f;
    public float DelayChangeGunAtual { get; set; }

    public const float DelayDropGun = 0.3f;
    public float DelayDropGunAtual { get; set; }

    public int MaxGuns = 3;
    public List<BaseGunModel> Guns { get; set; } = new();
    public BaseGunModel EquippedGun { get; set; }

    public List<BaseItemModel> Inventory { get; set; } = new();
    private bool IsInvOpen = false;

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

        Guns.Add(new PistolModel((0, 0)));
        Guns.Add(new SniperModel((0, 0)));
        Guns.Add(new ShotgunModel((0, 0)));
        Guns[1].User = this;
        Guns[2].User = this;
        EquippedGun = Guns[0];
        EquippedGun.User = this;
    }

    public override void Update(GameTime gameTime, List<BaseEntityModel> entities)
    {
        var teclado = Keyboard.GetState();
        var mouse = Mouse.GetState();

        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (EquippedGun != null) EquippedGun.Update(gameTime, entities);

        #region Delays

        DelayDanoAtual -= delta;
        DelayInvAtual -= delta;
        DelayChangeGunAtual -= delta;
        DelayDropGunAtual -= delta;

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

        #region Other Inputs

        var camera = GlobalVariables.GetService<ICameraService>();
        var mouseCamera = Vector2.Transform(new Vector2(mouse.X, mouse.Y), Matrix.Invert(camera.Transform));
        TargetDirection = mouseCamera - Position;

        if (mouse.LeftButton == ButtonState.Pressed && EquippedGun != null)
        {
            EquippedGun.Shoot();
        }

        if (teclado.IsKeyDown(Keys.E) && DelayInvAtual < 0)
        {
            IsInvOpen = !IsInvOpen;
            DelayInvAtual = DelayInv;
        }

        if (teclado.IsKeyDown(Keys.F) && DelayChangeGunAtual < 0)
        {
            ChangeGun();
            DelayChangeGunAtual = DelayChangeGun;
        }

        if (teclado.IsKeyDown(Keys.Q) && DelayDropGunAtual < 0 && EquippedGun != null)
        {
            var gunToDrop = EquippedGun;

            gunToDrop.IsDestroyed = false;
            gunToDrop.User = null;
            gunToDrop.Position = CenterPosition - gunToDrop.Size / 2;
            gunToDrop.Speed += new Vector2(1, 1);
            gunToDrop.Direction = TargetDirection;

            if (Guns.Count == 1)
                EquippedGun = null;
            else
                ChangeGun();

            Guns.Remove(gunToDrop);
            entities.Add(gunToDrop);

            DelayDropGunAtual = DelayDropGun;
        }

        #endregion

        base.Update(gameTime, entities);
    }

    private void ChangeGun()
    {
        if (Guns.Count <= 1) return;
        var idx = Guns.IndexOf(EquippedGun);
        EquippedGun = Guns[(idx + 1) % Guns.Count];
    }

    public override void Colision(BaseEntityModel model)
    {
        if (model is BaseCollectableModel colec && colec.Speed.LengthSquared() < 0.1f)
        {
            if (colec is BaseGunModel gun && Guns.Count < MaxGuns)
            {
                Guns.Add(gun);
                gun.User = this;
                gun.Destroy();

                if (EquippedGun == null) EquippedGun = gun;
            }
            else if (colec is BaseItemModel item)
            {
                Inventory.Add(item);
                item.Destroy();
            }
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

        if (model is PortalModel portal)
        {
            var mapService = GlobalVariables.GetService<IMapService>();

            mapService.GenerateMap(5);

            GlobalVariables.CurrentRoom.Entities.Add(this);

            Position = new((int)GlobalVariables.CurrentRoom.Size.X / 2, (int)GlobalVariables.CurrentRoom.Size.Y / 2);
        }

        base.Colision(model);
    }

    public override void Draw()
    {
        base.Draw();
        if (EquippedGun != null) DrawGun();

        DrawBar(0, MaxHealth, Health, Color.Red);
        DrawBar(1, MaxXp, Xp, Color.Purple);

        GlobalVariables.SpriteBatchInterface.DrawString(GlobalVariables.Font, $"Level: {Level}", new Vector2(10, 62), Color.White);

        GlobalVariables.SpriteBatchInterface.DrawString(GlobalVariables.Font, $"Coins: {Coins}", new Vector2(10, 86), Color.White);

        if (IsInvOpen) DrawInventory();
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

    private void DrawInventory()
    {
        var menuColor = Color.DarkGray * 0.9f;

        var width = (int)(GlobalVariables.Graphics.PreferredBackBufferWidth / 1.2);
        var height = (int)(GlobalVariables.Graphics.PreferredBackBufferHeight / 1.5);
        var x = (GlobalVariables.Graphics.PreferredBackBufferWidth / 2) - (width / 2);
        var y = 10;
        GlobalVariables.SpriteBatchInterface.Draw(GlobalVariables.Pixel, new Rectangle(x, y, width, height), menuColor);

        for (var i = 0; i < Inventory.Count; i++)
        {
            var item = Inventory[i];
            var itemX = x + 20 + (i % 5) * 70;
            var itemY = y + 20 + (i / 5) * 70;
            GlobalVariables.SpriteBatchInterface.Draw(GlobalVariables.Pixel, new Rectangle(itemX, itemY, 64, 64), item.Color);
        }

        var gunInvWidth = width / 3 - 10;
        var gunInvHeight = height / 3 - 10;
        var gunInvY = y + height + 10;

        for (var i = 0; i < MaxGuns; i++)
        {
            var gunInvX = x + i * (gunInvWidth + 15);

            var gunInvRect = new Rectangle(gunInvX, gunInvY, gunInvWidth, gunInvHeight);
            GlobalVariables.SpriteBatchInterface.Draw(GlobalVariables.Pixel, gunInvRect, menuColor);

            if (i < Guns.Count)
            {
                var gun = Guns[i];

                var gunInvRarityRect = new Rectangle(gunInvX + 10, gunInvY + 10, gunInvWidth - 20, gunInvHeight - 20);
                GlobalVariables.SpriteBatchInterface.Draw(GlobalVariables.Pixel, gunInvRarityRect, RngHelper.GetRarityColor(gun.Rarity) * 0.8f);

                var gunWidth = (int)gun.Size.X * 2;
                var gunHeight = (int)gun.Size.Y * 2;
                var gunX = gunInvX + gunInvWidth / 2 - gunWidth / 2;
                var gunY = gunInvY + gunInvHeight / 2 - gunHeight / 2;

                GlobalVariables.SpriteBatchInterface.Draw(GlobalVariables.Pixel, new Rectangle(gunX, gunY, gunWidth, gunHeight), gun.Color);
            }
        }
    }
}
