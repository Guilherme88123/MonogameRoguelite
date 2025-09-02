using Microsoft.Xna.Framework;

namespace Application.Dto;

public class NodeDto
{
    public Point Position { get; }
    public NodeDto Parent { get; set; }
    public float ValueOfInit { get; set; }
    public float Heuristica { get; set; }
    public float ValueTotal => ValueOfInit + Heuristica;

    public NodeDto(Point pos)
    {
        Position = pos;
    }
}
