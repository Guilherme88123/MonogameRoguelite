using Application.Model.Entities;
using Application.Model.Entities.Creature.Enemy;
using MonogameRoguelite.Model.Entities.Creature.Enemy;
using MonogameRoguelite.Model.Room.Base;
using System;
using System.Collections.Generic;

namespace Application.Model.Room;

public class ChestRoomModel : BaseRoomModel
{
    public ChestRoomModel() : base(10, 10)
    {
        Finished = true;
        DelayAfterFinish = 0f;
    }

    protected override Dictionary<int, Type> InitialEntities()
    {
        return new Dictionary<int, Type>()
        {
            { 1, typeof(ChestModel) }
        };
    }
}
