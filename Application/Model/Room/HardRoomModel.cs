using Application.Model.Entities.Creature.Enemy;
using System;
using System.Collections.Generic;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities;
using MonogameRoguelite.Model.Entities.Creature.Enemy;
using MonogameRoguelite.Model.Room.Base;

namespace MonogameRoguelite.Model.Room;

public class HardRoomModel : BaseRoomModel
{
    public HardRoomModel() : base(2000, 1500)
    {
        LoadInitialEntities(new Dictionary<int, Type>()
        {
            { 3, typeof(SlimeModel) },
            { 1, typeof(FlyModel) }
        });
    }

    protected override void AddObstacles()
    {
        var x = GlobalVariables.Graphics.PreferredBackBufferWidth / 2;

        for (var i = 2; i < 7; i++)
        {
            Entities.Add(new WallModel((x, GlobalVariables.Graphics.PreferredBackBufferHeight - 64 * i)));
        }
    }
}
