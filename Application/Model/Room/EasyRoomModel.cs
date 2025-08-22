using System;
using System.Collections.Generic;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities;
using MonogameRoguelite.Model.Entities.Creature.Enemy;
using MonogameRoguelite.Model.Room.Base;

namespace MonogameRoguelite.Model.Room;

public class EasyRoomModel : BaseRoomModel
{
    public EasyRoomModel() : base(1500, 1000)
    {
        LoadInitialEntities(new Dictionary<int, Type>()
        {
            { 2, typeof(SlimeModel) }
        });
    }

    protected override void AddObstacles()
    {
        var x = GlobalVariables.Graphics.PreferredBackBufferWidth / 3;

        for (var i = 0; i < 7; i++)
        {
            Entities.Add(new WallModel((x, 64 * i)));
        }
    }
}
