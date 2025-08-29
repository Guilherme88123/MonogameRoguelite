using System;
using System.Collections.Generic;
using MonogameRoguelite.Model.Entities.Creature.Enemy;
using MonogameRoguelite.Model.Room.Base;

namespace MonogameRoguelite.Model.Room;

public class EasyRoomModel : BaseRoomModel
{
    public EasyRoomModel() : base(21, 16)
    {
        LoadInitialEntities(new Dictionary<int, Type>()
        {
            { 2, typeof(SlimeModel) }
        });
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
