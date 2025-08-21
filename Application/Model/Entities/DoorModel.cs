using Microsoft.Xna.Framework;
using Teste001.Enum;
using Teste001.Model.Entities.Base;
using Teste001.Dto;

namespace Teste001.Model.Entities;

public class DoorModel : BaseEntityModel
{
    public readonly DirectionType DirectionPosition;

    public DoorModel(DirectionType direction) : base((0, 0))
    {
        DirectionPosition = direction;
        Color = Color.DarkSlateGray;
        Size = 96;
        Position = GetPosition();
    }

    private Vector2 GetPosition()
    {
        int x = 0;
        int y = 0;

        switch (DirectionPosition)
        {
            case DirectionType.Left:
                x = 15 - Size;
                y = GlobalVariables.Graphics.PreferredBackBufferHeight / 2 - (Size / 2);
                break;
            case DirectionType.Right:
                x = GlobalVariables.Graphics.PreferredBackBufferWidth - 15;
                y = GlobalVariables.Graphics.PreferredBackBufferHeight / 2 - (Size / 2);
                break;
            case DirectionType.Up:
                x = GlobalVariables.Graphics.PreferredBackBufferWidth / 2 - (Size / 2);
                y = 15 - Size;
                break;
            case DirectionType.Down:
                x = GlobalVariables.Graphics.PreferredBackBufferWidth / 2 - (Size / 2);
                y = GlobalVariables.Graphics.PreferredBackBufferHeight - 15;
                break;
        }

        return new Vector2(x, y);
    }
}
