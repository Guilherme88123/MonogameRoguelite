using Microsoft.Xna.Framework;
using Teste001.Model.Entities.Drop.Base;

namespace Teste001.Model.Entities.Drop.Coin;

public class CoinModel : BaseDropModel
{
    public CoinModel((int x, int y) position) : base(position)
    {
        Size = new Vector2(32, 32);
        Color = Color.Gold;
        Value = 1;
    }
}
