using Teste001.Enum;
using Teste001.Model.Entities.Creature.Player;
using Teste001.Model.Room.Base;

namespace Teste001.Interface;

public interface IMapService
{
    public BaseRoomModel CurrentRoom { get; set; }

    public void Move(DirectionType direction, PlayerModel player);
    public void DrawMap();
}
