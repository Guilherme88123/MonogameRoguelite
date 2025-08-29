using Application.Model.Entities;
using MonogameRoguelite.Model.Entities.Creature.Enemy;
using MonogameRoguelite.Model.Room.Base;
using System;
using System.Collections.Generic;

namespace Application.Model.Room;

public class ChestRoomModel : BaseRoomModel
{
    public ChestRoomModel() : base(10, 10)
    {
        LoadInitialEntities(new Dictionary<int, Type>()
        {
            { 1, typeof(ChestModel) }
        });

        Finished = true;
        DelayAfterFinish = 0f;
    }
}
