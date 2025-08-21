using Microsoft.Xna.Framework;
using System;
using Teste001.Dto;
using Teste001.Enum;
using Teste001.Interface;
using Teste001.Model.Entities;
using Teste001.Model.Entities.Creature.Player;
using Teste001.Model.Room;
using Teste001.Model.Room.Base;
using Teste001.Model.Room.Boss;
using Teste001.Model.Room.Initial;

namespace Teste001.Service.Room;

public class MapService : IMapService
{
    private static readonly Random Random = new();

    private BaseRoomModel[,] Rooms;
    private int X;
    private int Y;

    public BaseRoomModel CurrentRoom { get; set; }

    private static readonly (int X, int Y, DirectionType Description)[] Directions =
    {
        (1, 0, DirectionType.Right),
        (-1, 0, DirectionType.Left),
        (0, 1, DirectionType.Down),
        (0, -1, DirectionType.Up)
    };

    public MapService()
    {
        Rooms = GenerateRooms(5);//Pegar de opções
    }

    public BaseRoomModel[,] GenerateRooms(int width)
    {
        var rooms = new BaseRoomModel[width, width];
        var roomsCount = 1;
        int roomsNumber = (width * width) / 3;

        int middle = width / 2;
        rooms[middle, middle] = new InitialRoomModel();
        X = Y = middle;
        CurrentRoom = rooms[middle, middle];

        int oldX = middle;
        int oldY = middle;

        while (roomsCount < roomsNumber)
        {
            var (actualX, actualY, direc) = GetNextPosition(oldX, oldY, width);

            if (rooms[actualX, actualY] != null) continue;

            roomsCount++;

            if (roomsCount == roomsNumber)
            {
                rooms[actualX, actualY] = new BossRoomModel();
            }
            else
            {
                var difficulty = Random.Next(3);

                BaseRoomModel newRoom = difficulty switch
                {
                    0 => new EasyRoomModel(),
                    1 => new MediumRoomModel(),
                    2 => new HardRoomModel(),
                    _ => new EasyRoomModel()
                };

                rooms[actualX, actualY] = newRoom;
            }

            rooms[oldX, oldY].Entities.Add(new DoorModel(direc));
            rooms[actualX, actualY].Entities.Add(new DoorModel(GetAgainst(direc)));
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

    private DirectionType GetAgainst(DirectionType direc)
    {
        return direc switch
        {
            DirectionType.Up => DirectionType.Down,
            DirectionType.Down => DirectionType.Up,
            DirectionType.Right => DirectionType.Left,
            DirectionType.Left => DirectionType.Right,
        };
    }

    public void Move(DirectionType direc, PlayerModel player)
    {
        Vector2 newPosition = Vector2.Zero;

        switch (direc)
        {
            case DirectionType.Right:
                X += 1;
                newPosition = new Vector2(20,
                    GlobalVariables.Graphics.PreferredBackBufferHeight / 2 - (player.Size.Y / 2));
                break;
            case DirectionType.Left:
                X -= 1;
                newPosition = new Vector2(GlobalVariables.Graphics.PreferredBackBufferWidth - player.Size.X - 20,
                    GlobalVariables.Graphics.PreferredBackBufferHeight / 2 - (player.Size.Y / 2));
                break;
            case DirectionType.Up:
                Y -= 1;
                newPosition = new Vector2(GlobalVariables.Graphics.PreferredBackBufferWidth / 2 - (player.Size.X / 2),
                    GlobalVariables.Graphics.PreferredBackBufferHeight - player.Size.Y - 20);
                break;
            case DirectionType.Down:
                Y += 1;
                newPosition = new Vector2(GlobalVariables.Graphics.PreferredBackBufferWidth / 2 - (player.Size.X / 2),
                    20);
                break;
        }

        CurrentRoom.Entities.Remove(player);
        CurrentRoom = Rooms[X, Y];
        CurrentRoom.Visited = true;
        player.Position = newPosition;
        CurrentRoom.Entities.Add(player);
    }

    public void DrawMap()
    {
        var width = 15;
        var space = 5;
        var overlayWidth = space + Rooms.GetLength(0) * (width + space);
        var overlayY = 10;
        var overlayX = GlobalVariables.Graphics.PreferredBackBufferWidth - 10 - overlayWidth;

        GlobalVariables.SpriteBatch.Draw(GlobalVariables.Pixel, new Rectangle(overlayX, overlayY, overlayWidth, overlayWidth), Color.LightGray * 0.5f);

        for (int posX = 0; posX < Rooms.GetLength(0); posX++)
        {
            for (int posY = 0; posY < Rooms.GetLength(1); posY++)
            {
                var room = Rooms[posX, posY];
                if (room == null) continue;

                var y = overlayY + (posY * (width + space)) + space;
                var x = overlayX + (posX * (width + space)) + space;

                GlobalVariables.SpriteBatch.Draw(GlobalVariables.Pixel, new Rectangle(x, y, width, width), GetRoomColor(room));
            }
        }
    }

    private Color GetRoomColor(BaseRoomModel room)
    {
        return room switch
        {
            _ when room == CurrentRoom => Color.White,
            InitialRoomModel => Color.Blue,
            _ when room.Finished => Color.Green,
            _ when !room.Visited => Color.Transparent,
            BossRoomModel => Color.Red,
            _ => Color.Gray,
        };
    }
}
