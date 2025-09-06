using Application.Infrastructure;
using Application.Service.Collectable;
using Microsoft.Xna.Framework;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities.Base;
using MonogameRoguelite.Model.Entities.Creature.Player;

namespace Application.Model.Entities;

public class ChestModel : BaseEntityModel
{
    public ChestModel((float x, float y) position) : base(position)
    {
        Size = new Vector2(128, 48);
        Color = Color.SaddleBrown;

        Position = new Vector2(GlobalVariables.CurrentRoom.Size.X / 2 - Size.X / 2,
                               GlobalVariables.CurrentRoom.Size.Y / 2 - Size.Y / 2);
    }

    public override void Colision(BaseEntityModel model)
    {
        if (model is PlayerModel player)
        {
            var rarityitem = RngHelper.GetRandomRarity();

            var collec = CollectableFactory.GetRandomCollectable(rarityitem);

            collec.Position = CenterPosition;

            GlobalVariables.CurrentRoom.EntitiesToAdd.Add(collec);

            Destroy();
        }

        base.Colision(model);
    }
}
