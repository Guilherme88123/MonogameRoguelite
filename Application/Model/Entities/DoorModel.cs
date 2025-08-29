using Microsoft.Xna.Framework;
using MonogameRoguelite.Enum;
using MonogameRoguelite.Model.Entities.Base;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Room.Base;

namespace MonogameRoguelite.Model.Entities;

public class DoorModel : BaseEntityModel
{
    public readonly DirectionType DirectionPosition;
    public Vector2 RoomSize { get; set; }

    public DoorModel(DirectionType direction, BaseRoomModel room) : base((0, 0))
    {
        DirectionPosition = direction;
        Color = Color.DarkSlateGray;
        Size = new Vector2(96, 96);
        RoomSize = room.Size;
        Position = GetPosition();
        IsCollidable = false;
    }

    private Vector2 GetPosition()
    {
        float x = 0f;
        float y = 0f;

        var wallSize = new WallModel((0, 0)).Size.X;

        switch (DirectionPosition)
        {
            case DirectionType.Left:
                x = -wallSize;
                y = RoomSize.Y / 2 - (Size.Y / 2);
                break;
            case DirectionType.Right:
                x = RoomSize.X - (int)(Size.X - wallSize);
                y = RoomSize.Y / 2 - (Size.Y / 2);
                break;
            case DirectionType.Up:
                x = RoomSize.X / 2 - (Size.X / 2);
                y = -wallSize;
                break;
            case DirectionType.Down:
                x = RoomSize.X / 2 - (Size.X / 2);
                y = RoomSize.Y - (Size.Y - wallSize);
                break;
        }

        return new Vector2(x, y);
    }
}
