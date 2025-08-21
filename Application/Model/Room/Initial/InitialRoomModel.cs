using Teste001.Model.Room.Base;

namespace Teste001.Model.Room.Initial;

public class InitialRoomModel : BaseRoomModel
{
    public InitialRoomModel() : base(600, 400)
    {
        Finished = true;
    }
}
