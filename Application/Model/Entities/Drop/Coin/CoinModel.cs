using Microsoft.Xna.Framework;
using MonogameRoguelite.Model.Entities.Drop.Base;

namespace MonogameRoguelite.Model.Entities.Drop.Coin;

public class CoinModel : BaseDropModel
{
    public CoinModel((int x, int y) position) : base(position)
    {
        Size = new Vector2(24, 24);
        Color = Color.Gold;
        Value = 1;
    }
}
