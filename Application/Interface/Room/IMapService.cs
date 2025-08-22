using MonogameRoguelite.Enum;
using MonogameRoguelite.Model.Entities.Creature.Player;

namespace Application.Interface.Room;

public interface IMapService
{
    public void Move(DirectionType direction, PlayerModel player);
    public void DrawMap();
}
