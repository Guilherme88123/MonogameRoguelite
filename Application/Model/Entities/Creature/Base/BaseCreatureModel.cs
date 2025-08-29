using Application.Enum;
using Application.Infrastructure;
using Application.Model;
using Microsoft.Xna.Framework;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities.Base;
using System.Collections.Generic;
using System.Linq;

namespace MonogameRoguelite.Model.Entities.Creature.Base;

public abstract class BaseCreatureModel : BaseEntityModel
{
    public int MaxHealth { get; set; }
    public int Health { get; set; }
    public MoveType MoveStatus { get; set; } = MoveType.Idle;
    public Vector2 TargetDirection { get; set; } = new();

    public BaseCreatureModel((float x, float y) position, int maxHealth) : base(position)
    {
        MaxHealth = maxHealth;
        Health = MaxHealth;
        TargetDirection = Direction;
    }

    public override void Update(GameTime gameTime, List<BaseEntityModel> entities)
    {
        if (Health <= 0)
        {
            Destroy();
        }

        base.Update(gameTime, entities);
    }

    public List<Point> FindPath(Point start, Point goal)
    {
        var walls = GlobalVariables.CurrentRoom.Walls;

        int width = walls.GetLength(0);
        int height = walls.GetLength(1);

        var openList = new List<Node>();
        var closedList = new HashSet<Point>();

        Node startNode = new Node(start) { ValueOfInit = 0, Heuristica = VectorHelper.Heuristic(start, goal) };
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            // pega o nó com menor F
            Node current = openList.OrderBy(n => n.ValueTotal).First();

            if (current.Position == goal)
                return VectorHelper.ReconstructPath(current);

            openList.Remove(current);
            closedList.Add(current.Position);

            foreach (Point neighbor in VectorHelper.GetNeighbors(current.Position, width, height))
            {
                if (walls[neighbor.X, neighbor.Y] != null) // parede -> ignora
                    continue;
                if (closedList.Contains(neighbor))
                    continue;

                float tentativeG = current.ValueOfInit + 1;

                Node neighborNode = openList.FirstOrDefault(n => n.Position == neighbor);
                if (neighborNode == null)
                {
                    neighborNode = new Node(neighbor)
                    {
                        Parent = current,
                        ValueOfInit = tentativeG,
                        Heuristica = VectorHelper.Heuristic(neighbor, goal)
                    };
                    openList.Add(neighborNode);
                }
                else if (tentativeG < neighborNode.ValueOfInit)
                {
                    neighborNode.Parent = current;
                    neighborNode.ValueOfInit = tentativeG;
                }
            }
        }

        return new List<Point>(); // sem caminho
    }
}
