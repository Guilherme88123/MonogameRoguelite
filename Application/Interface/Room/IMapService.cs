using MonogameRoguelite.Enum;
using MonogameRoguelite.Model.Entities.Creature.Player;

namespace Application.Interface.Room;

public interface IMapService
{
    public void Move(DirectionType direction);
    public void DrawMap();
    public void GenerateMap();
    public void GoToNextFloor();
}
