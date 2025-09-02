using Application.Model.Entities;
using Application.Model.Entities.Creature.Enemy;
using MonogameRoguelite.Model.Entities.Creature.Enemy;
using MonogameRoguelite.Model.Room.Base;
using System;
using System.Collections.Generic;

namespace MonogameRoguelite.Model.Room;

public class EasyRoomModel : BaseRoomModel
{
    public EasyRoomModel() : base(21, 16)
    {
    }

    protected override Dictionary<Type, (int, int)> InitialEntities()
    {
        return new Dictionary<Type, (int, int)>()
        {
            { typeof(SlimeModel), (1, 2) },
            { typeof(CockroachModel), (0, 2) },
        };
    }

    protected override string[] AddObstacles()
    {
        return
        [
            ".......X.............",
            ".......X.............",
            ".......X.............",
            ".......X.............",
            ".......X......X......",
            ".......X......X......",
            ".......X......X......",
            ".......X......X......",
            ".......X......X......",
            ".......X......X......",
            ".......X......X......",
            ".......X......X......",
            "..............X......",
            "..............X......",
            "..............X......",
            "..............X......",
        ];
    }
}
