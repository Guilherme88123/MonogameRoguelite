using MonogameRoguelite.Model.Room.Base;

namespace MonogameRoguelite.Model.Room.Initial;

public class InitialRoomModel : BaseRoomModel
{
    public InitialRoomModel() : base(15 * 64, 10 * 64)
    {
        Finished = true;
        DelayAfterFinish = 0f;
    }
}
