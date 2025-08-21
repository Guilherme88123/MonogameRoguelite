using System;
using System.Collections.Generic;
using Teste001.Model.Entities.Creature.Enemy.Boss;
using Teste001.Model.Room.Base;

namespace Teste001.Model.Room.Boss;

public class BossRoomModel : BaseRoomModel
{
    public BossRoomModel() : base(1800, 1024)
    {
        LoadInitialEntities(new Dictionary<int, Type>()
        {
            { 1, typeof(KingSlimeModel) }
        });
    }
}
