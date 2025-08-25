using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameRoguelite.Model.Room.Base;
using System;

namespace MonogameRoguelite.Dto;

public static class GlobalVariables
{
    public static GraphicsDeviceManager Graphics;
    public static Texture2D Pixel;
    public static SpriteFont Font;

    public static SpriteBatch SpriteBatchEntities;
    public static SpriteBatch SpriteBatchInterface;

    public static Vector2 PlayerPosition = Vector2.Zero;
    public static BaseRoomModel CurrentRoom;
    public static Game Game;

    public static IServiceProvider ServiceProvider { get; set; }

    public static T GetService<T>() where T : notnull
        => ServiceProvider.GetRequiredService<T>();
}
