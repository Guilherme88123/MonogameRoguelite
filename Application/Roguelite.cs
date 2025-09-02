using Application.Interface.Room;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities.Creature.Player;
using Application.Interface.Camera;
using Application.Enum;
using Application.Interface.Menu;

namespace MonogameRoguelite;

public class Roguelite : Game
{
    private readonly IMapService MapService;
    private readonly ICameraService CameraService;
    private readonly IMenuService MenuService;

    private float EscDelay = 0.3f;
    private float EscDelayAtual = 0f;

    private GameStatusType GameStatus = GameStatusType.Playing;

    public Roguelite()
    {
        var graphics = new GraphicsDeviceManager(this);
        graphics.PreferredBackBufferWidth = 500;
        graphics.PreferredBackBufferHeight = 250;
        //graphics.IsFullScreen = true;
        IsFixedTimeStep = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1d / 120d);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        graphics.ApplyChanges();

        GlobalVariables.Graphics = graphics;

        MapService = GlobalVariables.GetService<IMapService>();
        CameraService = GlobalVariables.GetService<ICameraService>();
        MenuService = GlobalVariables.GetService<IMenuService>();
    }

    protected override void LoadContent()
    {
        var spriteBatchEntities = new SpriteBatch(GraphicsDevice);
        var spriteBatchInterface = new SpriteBatch(GraphicsDevice);

        var pixel = new Texture2D(GraphicsDevice, 1, 1);
        pixel.SetData([Color.White]);

        GlobalVariables.Font = Content.Load<SpriteFont>("DefaultFont");
        GlobalVariables.SpriteBatchEntities = spriteBatchEntities;
        GlobalVariables.SpriteBatchInterface = spriteBatchInterface;
        GlobalVariables.Pixel = pixel;
    }

    protected override void Initialize()
    {
        MapService.GenerateMap();

        var Player = new PlayerModel(((int)GlobalVariables.CurrentRoom.Size.X / 2, 
                                      (int)GlobalVariables.CurrentRoom.Size.Y / 2));

        GlobalVariables.Player = Player;
        GlobalVariables.CurrentRoom.Entities.Add(Player);

        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        var teclado = Keyboard.GetState();
        if (teclado.IsKeyDown(Keys.Escape) && EscDelayAtual < 0)
        {
            GameStatus = GameStatus == GameStatusType.MainMenu ? GameStatusType.Playing : GameStatusType.MainMenu;
            EscDelayAtual = EscDelay;
        }
            
        EscDelayAtual -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        if (GameStatus == GameStatusType.Playing)
        {
            GlobalVariables.CurrentRoom.Update(gameTime);
            CameraService.Follow(GlobalVariables.Player.CenterPosition);
        }
        else if (GameStatus == GameStatusType.MainMenu)
        {
            MenuService.Update();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        GlobalVariables.SpriteBatchEntities.Begin(transformMatrix: CameraService.Transform);
        GlobalVariables.SpriteBatchInterface.Begin();

        GlobalVariables.CurrentRoom.Draw();

        MapService.DrawMap();

        if (GameStatus == GameStatusType.MainMenu)
        {
            MenuService.DrawMenu();
        }

        GlobalVariables.SpriteBatchEntities.End();
        GlobalVariables.SpriteBatchInterface.End();

        base.Draw(gameTime);
    }
}
