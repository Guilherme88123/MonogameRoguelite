using Application.Model.Entities.Creature.Enemy;
using System;
using System.Collections.Generic;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities;
using MonogameRoguelite.Model.Entities.Creature.Enemy;
using MonogameRoguelite.Model.Room.Base;

namespace MonogameRoguelite.Model.Room;

public class HardRoomModel : BaseRoomModel
{
    public HardRoomModel() : base(23 * 64, 18 * 64)
    {
        LoadInitialEntities(new Dictionary<int, Type>()
        {
            { 3, typeof(SlimeModel) },
            { 1, typeof(FlyModel) }
        });
    }

    protected override void AddObstacles()
    {
        var x = Size.X / 3;
        var y = Size.Y / 3;
        var tileSize = WallModel.DefaultSize;

        Entities.Add(new WallModel((x, y), (x, y)));
    }
}
