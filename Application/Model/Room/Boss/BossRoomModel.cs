using System;
using System.Collections.Generic;
using MonogameRoguelite.Model.Entities.Creature.Enemy.Boss;
using MonogameRoguelite.Model.Room.Base;

namespace MonogameRoguelite.Model.Room.Boss;

public class BossRoomModel : BaseRoomModel
{
    public BossRoomModel() : base(2000, 2000)
    {
        LoadInitialEntities(new Dictionary<int, Type>()
        {
            { 1, typeof(KingSlimeModel) }
        });
    }
}
