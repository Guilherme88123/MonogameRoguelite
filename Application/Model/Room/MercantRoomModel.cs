using MonogameRoguelite.Model.Room.Base;

namespace Application.Model.Room;

public class MercantRoomModel : BaseRoomModel
{
    public MercantRoomModel() : base(20, 20)
    {
        Finished = true;
        DelayAfterFinish = 0f;
    }

    protected override string[] AddObstacles()
    {
        return
        [
            "....................",
            "....XX..............",
            "....XX..............",
            "....XX..............",
            "....XX..............",
            ".XXXXX..............",
            ".XXXXX..............",
            "....................",
            "....................",
            "....................",
            "....................",
            "....................",
            "....................",
            "..............XX....",
            ".............XXXX...",
            ".............XXXX...",
            "..............XX....",
            "....................",
            "....................",
            "....................",
        ];
    }
}
