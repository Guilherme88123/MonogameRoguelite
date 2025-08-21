using System;
using System.Collections.Generic;
using Teste001.Model.Entities.Creature.Enemy;
using Teste001.Model.Room.Base;

namespace Teste001.Model.Room;

public class EasyRoomModel : BaseRoomModel
{
    public EasyRoomModel() : base(800, 600)
    {
        LoadInitialEntities(new Dictionary<int, Type>()
        {
            { 2, typeof(SlimeModel) }
        });
    }
}
