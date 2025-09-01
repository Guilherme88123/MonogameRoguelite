using Microsoft.Xna.Framework;
using MonogameRoguelite.Enum;
using MonogameRoguelite.Model.Entities.Base;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Room.Base;
using Application.Model;

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

        var wallSize = WallModel.Size.X;

        switch (DirectionPosition)
        {
            case DirectionType.Left:
                x = 0;
                y = RoomSize.Y / 2 - (Size.Y / 2);
                break;
            case DirectionType.Right:
                x = RoomSize.X - Size.X;
                y = RoomSize.Y / 2 - (Size.Y / 2);
                break;
            case DirectionType.Up:
                x = RoomSize.X / 2 - (Size.X / 2);
                y = 0;
                break;
            case DirectionType.Down:
                x = RoomSize.X / 2 - (Size.X / 2);
                y = RoomSize.Y - Size.Y;
                break;
        }

        return new Vector2(x, y);
    }
}
