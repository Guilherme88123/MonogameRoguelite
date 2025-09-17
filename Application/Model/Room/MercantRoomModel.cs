using Application.Enum;
using Application.Infrastructure;
using Application.Model.Entities.Collectable.Base;
using Application.Model.Entities.Collectable.Item;
using Application.Service.Collectable;
using Microsoft.Xna.Framework;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Room.Base;
using System.Collections.Generic;
using System.Drawing;

namespace Application.Model.Room;

public class MercantRoomModel : BaseRoomModel
{
    public List<BaseCollectableModel> Items { get; set; } = new();

    public MercantRoomModel() : base(20, 20)
    {
        Finished = true;
        DelayAfterFinish = 0f;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void OnRoomEnter()
    {
        base.OnRoomEnter();

        Items.Add(CollectableFactory.GetRandomCollectable(RngHelper.GetRandomRarity()));
        Items.Add(CollectableFactory.GetRandomCollectable(RngHelper.GetRandomRarity()));
        Items.Add(CollectableFactory.GetRandomCollectable(RngHelper.GetRandomRarity()));

        Items[0].Position = new(256, 256);
        Items[0].Direction = new(0, 0);
        Items[0].IsCollidable = false;

        Items[1].Position = new(256, 512);
        Items[1].Direction = new(0, 0);
        Items[1].IsCollidable = false;

        Items[2].Position = new(512, 256);
        Items[2].Direction = new(0, 0);
        Items[2].IsCollidable = false;

        GlobalVariables.CurrentRoom.EntitiesToAdd.AddRange(Items);
    }

    protected override string[] AddObstacles()
    {
        return
        [
            "....................",
            "....XX..............",
            "....XX..............",
            "....XX..............",
            "....XX..............",
            ".XXXXX..............",
            ".XXXXX..............",
            "....................",
            "....................",
            "....................",
            "....................",
            "....................",
            "....................",
            "..............XX....",
            ".............XXXX...",
            ".............XXXX...",
            "..............XX....",
            "....................",
            "....................",
            "....................",
        ];
    }
}
