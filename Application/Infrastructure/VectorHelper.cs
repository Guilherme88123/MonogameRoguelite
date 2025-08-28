using Microsoft.Xna.Framework;
using System;

namespace Application.Infrastructure;

public static class VectorHelper
{
    public static Vector2 Rotate(Vector2 v, float angle)
    {
        float cos = (float)Math.Cos(angle);
        float sin = (float)Math.Sin(angle);
        return new Vector2(
            v.X * cos - v.Y * sin,
            v.X * sin + v.Y * cos
        );
    }
}
