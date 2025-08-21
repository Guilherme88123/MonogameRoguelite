using System;
using System.Collections.Generic;
using Teste001.Model.Entities.Creature.Enemy;
using Teste001.Model.Room.Base;

namespace Teste001.Model.Room;

public class MediumRoomModel : BaseRoomModel
{
    public MediumRoomModel() : base(800, 600)
    {
        LoadInitialEntities(new Dictionary<int, Type>()
        {
            { 3, typeof(SlimeModel) }
        });
    }
}
