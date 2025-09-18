using Application.Infrastructure;
using Application.Model.Entities.Collectable.Base;
using Application.Service.Collectable;
using Microsoft.Xna.Framework;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Room.Base;
using System.Collections.Generic;

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

        for (var i = 0; i < 3; i++)
        {
            var item = CollectableFactory.GetRandomCollectable(RngHelper.GetRandomRarity());

            item.Position = new(64 * (5 * (i + 1)) - item.Size.X / 2, 64 * 10 - item.Size.Y / 2);
            item.Direction = new(0, 0);
            item.RequireBuy = true;
            item.Price = RngHelper.GetPriceByRarity(item.Rarity);

            Items.Add(item);
        }

        GlobalVariables.CurrentRoom.EntitiesToAdd.AddRange(Items);
    }

    public override void Draw()
    {
        base.Draw();

        foreach (var item in Items)
        {
            if (!item.RequireBuy) continue;

            var text = $"Price: {item.Price}";

            var textNameSize = GlobalVariables.Font.MeasureString(text);

            GlobalVariables.SpriteBatchEntities.DrawString(GlobalVariables.Font, text, new Vector2(item.Position.X + item.Size.X / 2 - textNameSize.X / 2, item.Position.Y + item.Size.Y + textNameSize.Y + 5), Color.White);
        }
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
