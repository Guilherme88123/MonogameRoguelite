using Microsoft.Xna.Framework;

namespace Application.Model;

public class Node
{
    public Point Position { get; }
    public Node Parent { get; set; }
    public float ValueOfInit { get; set; }
    public float Heuristica { get; set; }
    public float ValueTotal => ValueOfInit + Heuristica;

    public Node(Point pos)
    {
        Position = pos;
    }
}
