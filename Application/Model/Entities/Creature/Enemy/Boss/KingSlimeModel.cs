using Microsoft.Xna.Framework;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Model.Entities.Creature.Enemy.Base;
using MonogameRoguelite.Model.Entities.Drop.Coin;
using MonogameRoguelite.Model.Entities.Drop.Xp;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace MonogameRoguelite.Model.Entities.Creature.Enemy.Boss;

public class KingSlimeModel : BaseEnemyModel
{
    public KingSlimeModel((float x, float y) position) : base(position, 50)
    {
        Size = new Vector2(128, 128);
        Acceleration = 600f;
        Friction = 320f;
        MaxSpeed = 120f;
        Color = Color.DarkGreen;
        VisionRange = 1000f;
        Name = "King Slime";
    }

    protected override Dictionary<Type, int> Drops()
    {
        return new()
        {
            { typeof(CoinModel), 3 },
            { typeof(XpNodeModel), 2 },
        };
    }

    public override void Draw()
    {
        base.Draw();

        var x = (int)GlobalVariables.Graphics.PreferredBackBufferWidth / 4;
        var width = x * 2;
        var y = 20;
        var height = 30;

        GlobalVariables.SpriteBatchInterface.Draw(GlobalVariables.Pixel, new Rectangle(x, y, width, height), Color.DarkGray);

        var healthWidth = (int)((Health / (float)MaxHealth) * (width - 10));
        var healthX = x + 5;
        var healthY = y + 5;
        var healthHeight = height - 10;
        GlobalVariables.SpriteBatchInterface.Draw(GlobalVariables.Pixel, new Rectangle(healthX, healthY, healthWidth, healthHeight), Color.Red);

        var textName = Name;
        var textHealth = $"{Health}/{MaxHealth}";

        var textNameSize = GlobalVariables.Font.MeasureString(textName);
        var textHealthSize = GlobalVariables.Font.MeasureString(textHealth);

        GlobalVariables.SpriteBatchInterface.DrawString(GlobalVariables.Font, textName, new Vector2(healthX + 5, healthY + healthHeight / 2 - textNameSize.Y / 2), Color.White);
        GlobalVariables.SpriteBatchInterface.DrawString(GlobalVariables.Font, textHealth, new Vector2(x + width / 2 - textHealthSize.X / 2, healthY + healthHeight / 2 - textHealthSize.Y / 2), Color.White);
    }
}
