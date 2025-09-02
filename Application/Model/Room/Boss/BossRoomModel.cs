using Application.Model.Entities;
using MonogameRoguelite.Model.Entities.Creature.Enemy.Boss;
using MonogameRoguelite.Model.Room.Base;
using System;
using System.Collections.Generic;

namespace MonogameRoguelite.Model.Room.Boss;

public class BossRoomModel : BaseRoomModel
{
    public BossRoomModel() : base(25, 20)
    {
    }

    protected override Dictionary<Type, (int, int)> InitialEntities()
    {
        return new Dictionary<Type, (int, int)>()
        {
            { typeof(KingSlimeModel), (1, 2) },
        };
    }

    protected override string[] AddObstacles()
    {
        return
        [
            ".........................",
            ".........................",
            ".........................",
            "...XXX.............XXX...",
            "...XXX.............XXX...",
            "...XXX.............XXX...",
            ".........................",
            ".........................",
            ".........................",
            ".........................",
            ".........................",
            ".........................",
            ".........................",
            ".........................",
            "...XXX.............XXX...",
            "...XXX.............XXX...",
            "...XXX.............XXX...",
            ".........................",
            ".........................",
            ".........................",
        ];
    }

    protected override void HasFinished()
    {
        var portal = new PortalModel((0, 0));
        portal.Position = new((Size.X - portal.Size.X) / 2, (Size.Y - portal.Size.Y) / 2);
        EntitiesToAdd.Add(portal);
        
        base.HasFinished();
    }
}
