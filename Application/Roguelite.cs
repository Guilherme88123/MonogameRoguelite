using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Teste001.Dto;
using Teste001.Interface;
using Teste001.Model.Entities.Creature.Player;

namespace Teste001;

public class Roguelite : Game
{
    private IMapService MapService;

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
    }

    protected override void LoadContent()
    {
        var spriteBatch = new SpriteBatch(GraphicsDevice);

        var pixel = new Texture2D(GraphicsDevice, 1, 1);
        pixel.SetData([Color.White]);

        //_font = Content.Load<SpriteFont>("DefaultFont");
        GlobalVariables.SpriteBatch = spriteBatch;
        GlobalVariables.Pixel = pixel;
    }

    protected override void Initialize()
    {
        var Player = new PlayerModel((GlobalVariables.Graphics.PreferredBackBufferWidth / 2, GlobalVariables.Graphics.PreferredBackBufferHeight / 2));

        MapService.CurrentRoom.Entities.Add(Player);

        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        var teclado = Keyboard.GetState();
        if (teclado.IsKeyDown(Keys.Escape))
            Exit();

        MapService.CurrentRoom.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.MediumSeaGreen);

        GlobalVariables.SpriteBatch.Begin();

        MapService.CurrentRoom.Draw();
        MapService.DrawMap();

        GlobalVariables.SpriteBatch.End();

        base.Draw(gameTime);
    }
}
