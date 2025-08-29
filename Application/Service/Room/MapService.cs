using Application.Interface.Room;
using Microsoft.Xna.Framework;
using System;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Enum;
using MonogameRoguelite.Model.Entities;
using MonogameRoguelite.Model.Room;
using MonogameRoguelite.Model.Room.Base;
using MonogameRoguelite.Model.Room.Boss;
using MonogameRoguelite.Model.Room.Initial;
using Application.Model.Room;

namespace MonogameRoguelite.Service.Room;

public class MapService : IMapService
{
    private static readonly Random Random = new();

    private BaseRoomModel[,] Rooms;
    private int X;
    private int Y;

    private static readonly (int X, int Y, DirectionType Description)[] Directions =
    {
        (1, 0, DirectionType.Right),
        (-1, 0, DirectionType.Left),
        (0, 1, DirectionType.Down),
        (0, -1, DirectionType.Up)
    };

    public void GenerateMap()
    {
        Rooms = GenerateRooms(GlobalVariables.MapSize);
    }

    public BaseRoomModel[,] GenerateRooms(int width)
    {
        var rooms = new BaseRoomModel[width, width];
        var roomsCount = 1;
        int roomsNumber = (width * width) / 3;

        int middle = width / 2;
        rooms[middle, middle] = new InitialRoomModel();
        X = Y = middle;
        GlobalVariables.CurrentRoom = rooms[middle, middle];
        GlobalVariables.CurrentRoom.Visited = true;

        int oldX = middle;
        int oldY = middle;

        while (roomsCount <= roomsNumber)
        {
            var (actualX, actualY, direc) = GetNextPosition(oldX, oldY, width);

            if (rooms[actualX, actualY] != null) continue;

            if (roomsCount == roomsNumber)
            {
                rooms[actualX, actualY] = new BossRoomModel();
            }
            else
            {
                BaseRoomModel newRoom = GetRandomRoom();

                rooms[actualX, actualY] = newRoom;
                if (roomsCount == 1) newRoom.Visited = true;
            }

            roomsCount++;

            rooms[oldX, oldY].Entities.Add(new DoorModel(direc, rooms[oldX, oldY]));
            rooms[oldX, oldY].NextRoomPosition = new Vector2(actualX, actualY);
            rooms[actualX, actualY].Entities.Add(new DoorModel(GetAgainstDirection(direc), rooms[actualX, actualY]));
            (oldX, oldY) = (actualX, actualY);
        }

        return rooms;
    }

    private (int, int, DirectionType) GetNextPosition(int actualX, int actualY, int maxWidth)
    {
        while (true)
        {
            var direc = Directions[Random.Next(Directions.Length)];

            int newX = actualX + direc.X;
            int newY = actualY + direc.Y;

            if (newX >= 0 && newX < maxWidth &&
                newY >= 0 && newY < maxWidth)
            {
                return (newX, newY, direc.Description);
            }
        }
    }

    private DirectionType GetAgainstDirection(DirectionType direc)
    {
        return direc switch
        {
            DirectionType.Up => DirectionType.Down,
            DirectionType.Down => DirectionType.Up,
            DirectionType.Right => DirectionType.Left,
            DirectionType.Left => DirectionType.Right,
        };
    }

    public void Move(DirectionType direc)
    {
        Vector2 newPosition = Vector2.Zero;

        var player = GlobalVariables.Player;

        switch (direc)
        {
            case DirectionType.Right:
                X += 1;
                newPosition = new Vector2(40,
                    Rooms[X, Y].Size.Y / 2 - (player.Size.Y / 2));
                break;
            case DirectionType.Left:
                X -= 1;
                newPosition = new Vector2(Rooms[X, Y].Size.X - player.Size.X - 40,
                    Rooms[X, Y].Size.Y / 2 - (player.Size.Y / 2));
                break;
            case DirectionType.Up:
                Y -= 1;
                newPosition = new Vector2(Rooms[X, Y].Size.X / 2 - (player.Size.X / 2),
                    Rooms[X, Y].Size.Y - player.Size.Y - 40);
                break;
            case DirectionType.Down:
                Y += 1;
                newPosition = new Vector2(Rooms[X, Y].Size.X / 2 - (player.Size.X / 2),
                    40);
                break;
        }

        GlobalVariables.CurrentRoom.Entities.Remove(player);
        GlobalVariables.CurrentRoom = Rooms[X, Y];
        player.Position = newPosition;
        GlobalVariables.CurrentRoom.Entities.Add(player);
        GlobalVariables.CurrentRoom.Visited = true;
        if (GlobalVariables.CurrentRoom.NextRoomPosition != Vector2.Zero) Rooms[(int)GlobalVariables.CurrentRoom.NextRoomPosition.X, (int)GlobalVariables.CurrentRoom.NextRoomPosition.Y].Visited = true;
    }

    public void DrawMap()
    {
        var width = 15;
        var space = 5;
        var overlayWidth = space + Rooms.GetLength(0) * (width + space);
        var overlayY = 10;
        var overlayX = GlobalVariables.Graphics.PreferredBackBufferWidth - 10 - overlayWidth;

        GlobalVariables.SpriteBatchInterface.Draw(GlobalVariables.Pixel, new Rectangle(overlayX, overlayY, overlayWidth, overlayWidth), Color.LightGray * 0.5f);

        for (int posX = 0; posX < Rooms.GetLength(0); posX++)
        {
            for (int posY = 0; posY < Rooms.GetLength(1); posY++)
            {
                var room = Rooms[posX, posY];
                if (room == null) continue;

                var y = overlayY + (posY * (width + space)) + space;
                var x = overlayX + (posX * (width + space)) + space;

                GlobalVariables.SpriteBatchInterface.Draw(GlobalVariables.Pixel, new Rectangle(x, y, width, width), GetRoomColor(room));
            }
        }
    }

    private Color GetRoomColor(BaseRoomModel room)
    {
        return room switch
        {
            _ when room == GlobalVariables.CurrentRoom => Color.White,
            _ when !room.Visited => Color.Transparent,
            InitialRoomModel => Color.Blue,
            BossRoomModel => Color.Red,
            ChestRoomModel => Color.Gold,
            _ when room.Finished => Color.Green,
            _ => Color.Gray,
        };
    }

    private BaseRoomModel GetRandomRoom()
    {
        var x = Random.Next(12);

        return x switch
        {
            _ when 0 >= x && x <= 3 => new EasyRoomModel(),
            _ when 4 >= x && x <= 6 => new MediumRoomModel(),
            _ when 7 >= x && x <= 9 => new HardRoomModel(),
            _ when x >= 10 => new ChestRoomModel(),
            _ => new EasyRoomModel()
        };
    }

    public void GoToNextFloor()
    {
        GlobalVariables.Flor += 1;

        GenerateMap();

        GlobalVariables.CurrentRoom.Entities.Add(GlobalVariables.Player);

        GlobalVariables.Player.Position = new((int)GlobalVariables.CurrentRoom.Size.X / 2, (int)GlobalVariables.CurrentRoom.Size.Y / 2);
    }
}
