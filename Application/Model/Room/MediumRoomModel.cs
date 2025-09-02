using Application.Model;
using Application.Model.Entities.Creature.Enemy;
using Application.Model.Entities.Creature.Enemy.Ranged;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities.Creature.Enemy;
using MonogameRoguelite.Model.Room.Base;
using System;
using System.Collections.Generic;

namespace MonogameRoguelite.Model.Room;

public class MediumRoomModel : BaseRoomModel
{
    public MediumRoomModel() : base(21, 16)
    {
    }

    protected override Dictionary<Type, (int, int)> InitialEntities()
    {
        return new Dictionary<Type, (int, int)>()
        {
            { typeof(SlimeModel), (1, 2) },
            { typeof(FlyModel), (1, 1) },
            { typeof(SlothModel), (0, 1) },
        };
    }

    protected override string[] AddObstacles()
    {
        return
        [
            ".....................",
            ".....................",
            ".....................",
            ".....................",
            "....XXXXXXXXXXXXX....",
            ".....................",
            ".....................",
            ".....................",
            ".....................",
            ".....................",
            ".....................",
            "....XXXXXXXXXXXXX....",
            ".....................",
            ".....................",
            ".....................",
            ".....................",
        ];
    }
}
