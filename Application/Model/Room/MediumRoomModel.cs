using Application.Model.Entities.Creature.Enemy;
using System;
using System.Collections.Generic;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities;
using MonogameRoguelite.Model.Entities.Creature.Enemy;
using MonogameRoguelite.Model.Room.Base;

namespace MonogameRoguelite.Model.Room;

public class MediumRoomModel : BaseRoomModel
{
    public MediumRoomModel() : base(20 * 64, 15 * 64)
    {
        LoadInitialEntities(new Dictionary<int, Type>()
        {
            { 2, typeof(SlimeModel) },
            { 1, typeof(FlyModel) }
        });
    }

    protected override void AddObstacles()
    {
        var x = Size.X / 4;
        var y = Size.Y / 5;
        var tileSize = WallModel.DefaultSize;

        Entities.Add(new WallModel((x, y), (x * 2, tileSize)));
        Entities.Add(new WallModel((x, y * 3), (x * 2, tileSize)));
    }
}
