using Application.Dto;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

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

    public static float Heuristic(Point a, Point b)
    {
        // Manhattan distance (bom pra grid ortogonal)
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }

    public static IEnumerable<Point> GetNeighbors(Point pos, int width, int height)
    {
        Point[] directions =
        {
            new Point(1, 0),   // right
            new Point(-1, 0),  // left
            new Point(0, 1),   // down
            new Point(0, -1),  // up
            new Point(1, -1),  // right-up
            new Point(-1, -1), // left-up
            new Point(1, 1),   // rigth-down
            new Point(-1, 1),  // left-down
        };

        foreach (var d in directions)
        {
            int nx = pos.X + d.X;
            int ny = pos.Y + d.Y;

            if (nx >= 0 && ny >= 0 && nx < width && ny < height)
                yield return new Point(nx, ny);
        }
    }

    public static List<Point> ReconstructPath(NodeDto node)
    {
        List<Point> path = new List<Point>();
        while (node != null)
        {
            path.Add(node.Position);
            node = node.Parent;
        }
        path.Reverse();
        return path;
    }
}
