using Application.Model.Entities.Creature.Enemy;
using System;
using System.Collections.Generic;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities.Creature.Enemy;
using MonogameRoguelite.Model.Room.Base;
using Application.Model;

namespace MonogameRoguelite.Model.Room;

public class HardRoomModel : BaseRoomModel
{
    public HardRoomModel() : base(23, 18)
    {
    }

    protected override Dictionary<int, Type> InitialEntities()
    {
        return new Dictionary<int, Type>()
        {
            { 3, typeof(SlimeModel) },
            { 1, typeof(FlyModel) }
        };
    }

    protected override string[] AddObstacles()
    {
        return
        [
            ".......................",
            ".......................",
            ".......................",
            ".......................",
            "....XXXXXXXXXXXXXXX....",
            "....XXXXXXXXXXXXXXX....",
            "....XXXXXXXXXXXXXXX....",
            "....XXXXXXXXXXXXXXX....",
            "....XXXXXXXXXXXXXXX....",
            "....XXXXXXXXXXXXXXX....",
            "....XXXXXXXXXXXXXXX....",
            "....XXXXXXXXXXXXXXX....",
            "....XXXXXXXXXXXXXXX....",
            "....XXXXXXXXXXXXXXX....",
            ".......................",
            ".......................",
            ".......................",
            ".......................",
        ];
    }
}
