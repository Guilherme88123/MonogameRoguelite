using MonogameRoguelite.Model.Room.Base;

namespace MonogameRoguelite.Model.Room.Initial;

public class InitialRoomModel : BaseRoomModel
{
    public InitialRoomModel() : base(15, 10)
    {
        Finished = true;
        DelayAfterFinish = 0f;
    }
}
