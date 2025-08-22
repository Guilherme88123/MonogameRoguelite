using System;
using System.Collections.Generic;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities;
using MonogameRoguelite.Model.Entities.Creature.Enemy;
using MonogameRoguelite.Model.Room.Base;

namespace MonogameRoguelite.Model.Room;

public class EasyRoomModel : BaseRoomModel
{
    public EasyRoomModel() : base(20 * 64, 15 * 64)
    {
        LoadInitialEntities(new Dictionary<int, Type>()
        {
            { 2, typeof(SlimeModel) }
        });
    }

    protected override void AddObstacles()
    {
        var x = Size.X / 3;
        var y = Size.Y / 3;
        var tileSize = WallModel.DefaultSize;

        Entities.Add(new WallModel((x, 0), (tileSize, y * 2)));
        Entities.Add(new WallModel((x * 2, Size.Y - y * 2), (tileSize, y * 2)));
    }
}
