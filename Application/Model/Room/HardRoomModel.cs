using Application.Model;
using Application.Model.Entities.Creature.Enemy;
using Application.Model.Entities.Creature.Enemy.Ranged;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities.Creature.Enemy;
using MonogameRoguelite.Model.Room.Base;
using System;
using System.Collections.Generic;

namespace MonogameRoguelite.Model.Room;

public class HardRoomModel : BaseRoomModel
{
    public HardRoomModel() : base(23, 18)
    {
    }

    protected override Dictionary<Type, (int, int)> InitialEntities()
    {
        return new Dictionary<Type, (int, int)>()
        {
            { typeof(SlimeModel), (2, 3) },
            { typeof(RatModel), (0, 1) },
            { typeof(CockroachModel), (1, 2) },
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
            ".......................",
            ".....XXXXXXXXXXXXX.....",
            ".....XXXXXXXXXXXXX.....",
            ".....XXXXXXXXXXXXX.....",
            ".....XXXXXXXXXXXXX.....",
            ".....XXXXXXXXXXXXX.....",
            ".....XXXXXXXXXXXXX.....",
            ".....XXXXXXXXXXXXX.....",
            ".....XXXXXXXXXXXXX.....",
            ".......................",
            ".......................",
            ".......................",
            ".......................",
            ".......................",
        ];
    }
}
