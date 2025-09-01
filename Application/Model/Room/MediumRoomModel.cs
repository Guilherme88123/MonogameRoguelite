using Application.Model.Entities.Creature.Enemy;
using System;
using System.Collections.Generic;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities.Creature.Enemy;
using MonogameRoguelite.Model.Room.Base;
using Application.Model;

namespace MonogameRoguelite.Model.Room;

public class MediumRoomModel : BaseRoomModel
{
    public MediumRoomModel() : base(21, 16)
    {
    }

    protected override Dictionary<int, Type> InitialEntities()
    {
        return new Dictionary<int, Type>()
        {
            { 2, typeof(SlimeModel) },
            { 1, typeof(FlyModel) }
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
