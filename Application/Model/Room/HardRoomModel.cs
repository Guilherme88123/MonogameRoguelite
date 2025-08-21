using Application.Model.Entities.Creature.Enemy;
using System;
using System.Collections.Generic;
using Teste001.Dto;
using Teste001.Model.Entities;
using Teste001.Model.Entities.Creature.Enemy;
using Teste001.Model.Room.Base;

namespace Teste001.Model.Room;

public class HardRoomModel : BaseRoomModel
{
    public HardRoomModel() : base(800, 600)
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
