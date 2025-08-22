using Application.Model.Entities.Creature.Enemy;
using System;
using System.Collections.Generic;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities;
using MonogameRoguelite.Model.Entities.Creature.Enemy;
using MonogameRoguelite.Model.Room.Base;

namespace MonogameRoguelite.Model.Room;

public class MediumRoomModel : BaseRoomModel
{
    public MediumRoomModel() : base(1800, 1500)
    {
        LoadInitialEntities(new Dictionary<int, Type>()
        {
            { 2, typeof(SlimeModel) },
            { 1, typeof(FlyModel) }
        });
    }

    protected override void AddObstacles()
    {
        var x = GlobalVariables.Graphics.PreferredBackBufferWidth / 3;

        for (var i = 1; i < 6; i++)
        {
            Entities.Add(new WallModel((x, GlobalVariables.Graphics.PreferredBackBufferHeight - 64 * i)));
        }
    }
}
