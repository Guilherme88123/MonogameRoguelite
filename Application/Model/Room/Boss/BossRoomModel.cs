using Application.Model.Entities;
using MonogameRoguelite.Model.Entities;
using MonogameRoguelite.Model.Entities.Creature.Enemy.Boss;
using MonogameRoguelite.Model.Room.Base;
using System;
using System.Collections.Generic;

namespace MonogameRoguelite.Model.Room.Boss;

public class BossRoomModel : BaseRoomModel
{
    public BossRoomModel() : base(25 * 64, 20 * 64)
    {
        LoadInitialEntities(new Dictionary<int, Type>()
        {
            { 1, typeof(KingSlimeModel) }
        });
    }

    protected override void AddObstacles()
    {
        var x = Size.X / 5;
        var y = Size.Y / 5;
        var tileSize = WallModel.DefaultSize;

        Entities.Add(new WallModel((x, y), (tileSize * 2, tileSize * 2)));
        Entities.Add(new WallModel((Size.X - x - tileSize * 2, y), (tileSize * 2, tileSize * 2)));
        Entities.Add(new WallModel((x, Size.Y - y - tileSize * 2), (tileSize * 2, tileSize * 2)));
        Entities.Add(new WallModel((Size.X - x - tileSize * 2, Size.Y - y - tileSize * 2), (tileSize * 2, tileSize * 2)));
    }

    protected override void HasFinished()
    {
        var portal = new PortalModel((0, 0));
        portal.Position = new((Size.X - portal.Size.X) / 2, (Size.Y - portal.Size.Y) / 2);
        EntitiesToAdd.Add(portal);
        
        base.HasFinished();
    }
}
