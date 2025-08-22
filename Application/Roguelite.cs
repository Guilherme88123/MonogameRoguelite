using Application.Interface.Room;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities.Creature.Player;
using Application.Interface.Camera;

namespace MonogameRoguelite;

public class Roguelite : Game
{
    private readonly IMapService MapService;
    private readonly ICameraService CameraService;

    public Roguelite()
    {
        var graphics = new GraphicsDeviceManager(this);
        graphics.PreferredBackBufferWidth = 800;
        graphics.PreferredBackBufferHeight = 600;
        IsFixedTimeStep = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1d / 120d);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        graphics.ApplyChanges();

        GlobalVariables.Graphics = graphics;

        MapService = GlobalVariables.GetService<IMapService>();
        CameraService = GlobalVariables.GetService<ICameraService>();
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
        var Player = new PlayerModel((GlobalVariables.Graphics.PreferredBackBufferWidth / 2, GlobalVariables.Graphics.PreferredBackBufferHeight / 2));

        GlobalVariables.CurrentRoom.Entities.Add(Player);

        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        var teclado = Keyboard.GetState();
        if (teclado.IsKeyDown(Keys.Escape))
            Exit();

        GlobalVariables.CurrentRoom.Update(gameTime);

        CameraService.Follow(GlobalVariables.PlayerPosition);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        GlobalVariables.SpriteBatchEntities.Begin(transformMatrix: CameraService.Transform);
        GlobalVariables.SpriteBatchInterface.Begin();

        GlobalVariables.CurrentRoom.Draw();

        MapService.DrawMap();

        GlobalVariables.SpriteBatchEntities.End();
        GlobalVariables.SpriteBatchInterface.End();

        base.Draw(gameTime);
    }
}
