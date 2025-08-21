using System;
using System.Collections.Generic;
using Teste001.Model.Entities.Creature.Enemy;
using Teste001.Model.Room.Base;

namespace Teste001.Model.Room;

public class HardRoomModel : BaseRoomModel
{
    public HardRoomModel() : base(800, 600)
    {
        LoadInitialEntities(new Dictionary<int, Type>()
        {
            { 4, typeof(SlimeModel) }
        });
    }
}
