using Application.Interface.Camera;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameRoguelite.Dto;

namespace Application.Service.Camera;

public class CameraService : ICameraService
{
    public Matrix Transform { get; private set; }
    public Vector2 Position { get; private set; }
    public float Zoom { get; set; } = 1f;
    public float Rotation { get; set; } = 0f;

    public void Follow(Vector2 target)
    {
        Position = target;

        // centraliza no meio da tela
        var cameraPosition = Matrix.CreateTranslation(
            -target.X,
            -target.Y,
            0);

        var offset = Matrix.CreateTranslation(
            GlobalVariables.Graphics.PreferredBackBufferWidth / 2f,
            GlobalVariables.Graphics.PreferredBackBufferHeight / 2f,
            0);

        var zoom = Matrix.CreateScale(Zoom);

        Transform = cameraPosition * zoom * offset;
    }
}
