using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Teste001.Dto;

public static class GlobalVariables
{
    public static GraphicsDeviceManager Graphics;
    public static SpriteBatch SpriteBatch;
    public static Texture2D Pixel;
    public static SpriteFont Font;
    public static Vector2 PlayerPosition = Vector2.Zero;
    
    public static IServiceProvider ServiceProvider { get; set; }

    public static T GetService<T>() where T : notnull
        => ServiceProvider.GetRequiredService<T>();
}
