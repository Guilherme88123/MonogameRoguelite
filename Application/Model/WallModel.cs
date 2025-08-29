using Microsoft.Xna.Framework;
using MonogameRoguelite.Dto;

namespace Application.Model;

public class WallModel
{
    public static Vector2 Size = new(64, 64);
    public static Color Color = new Color(103, 71, 54);
    public Vector2 Position;
    public Rectangle Rectangle => new((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

    public virtual void Draw(int x, int y)
    {
        Position = new Vector2(x * Size.X, y * Size.Y);

        GlobalVariables.SpriteBatchEntities.Draw(GlobalVariables.Pixel, Rectangle, Color);
    }
}
