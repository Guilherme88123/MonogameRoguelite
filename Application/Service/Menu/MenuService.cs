using Application.Interface.Menu;
using Application.Model.MenuElements.Base;
using Application.Model.MenuElements.Button;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonogameRoguelite.Dto;
using System.Collections.Generic;

namespace Application.Service.Menu;

public class MenuService : IMenuService
{
    private List<BaseElementModel> MenuElements = new();
    private Rectangle Rectangle = new();

    private List<(int, int)> WindowsSizes = new()
    {
        (800, 600),
        (1024, 768),
        (1280, 720),
        (1920, 1080)
    };

    private int CurrentWindowSizeIndex = 0;

    public MenuService()
    {
        AtualizarMenu();
    }

    public void DrawMenu()
    {
        GlobalVariables.SpriteBatchInterface.Draw(GlobalVariables.Pixel, Rectangle, Color.DarkGray);
        
        for (var i = 0; i < MenuElements.Count; i++)
        {
            var element = MenuElements[i];
            var elementX = Rectangle.X + (Rectangle.Width / 2) - (element.Rectangle.Width / 2);
            var elementY = Rectangle.Y + 20 + (i * (element.Rectangle.Height + 10));
            element.Draw(elementX, elementY);
        }
    }

    public void Update()
    {
        var mouse = Mouse.GetState();
        var mousePos = new Point(mouse.X, mouse.Y);

        foreach (var element in MenuElements)
        {
            var hovering = element.Rectangle.Contains(mousePos);
            if (hovering)
            {
                element.IsHover = true;

                if (mouse.LeftButton == ButtonState.Pressed)
                    element.Click?.Invoke();
            }
            else
            {
                element.IsHover = false;
            }
        }
    }

    private void ToggleWindowSize()
    {
        CurrentWindowSizeIndex = (CurrentWindowSizeIndex + 1) % WindowsSizes.Count;
        var (width, height) = WindowsSizes[CurrentWindowSizeIndex];
        GlobalVariables.Graphics.PreferredBackBufferWidth = width;
        GlobalVariables.Graphics.PreferredBackBufferHeight = height;
        GlobalVariables.Graphics.ApplyChanges();
        AtualizarMenu();
    }

    private void AtualizarMenu()
    {
        MenuElements.Clear();

        var width = GlobalVariables.Graphics.PreferredBackBufferWidth / 2;
        var height = GlobalVariables.Graphics.PreferredBackBufferHeight - 100;
        var x = GlobalVariables.Graphics.PreferredBackBufferWidth / 4;
        var y = 50;

        Rectangle = new Rectangle(x, y, width, height);

        var windowSize = new ButtonModel();
        windowSize.Rectangle = new Rectangle(0, 0, width - 20, 50);
        windowSize.Text = $"Toggle Window Size";
        windowSize.Click += () => ToggleWindowSize();

        var fullscreen = new ButtonModel();
        fullscreen.Rectangle = new Rectangle(0, 0, width - 20, 50);
        fullscreen.Text = "Toggle Fullscreen";
        fullscreen.Click += () => GlobalVariables.Graphics.ToggleFullScreen();

        var exit = new ButtonModel();
        exit.Rectangle = new Rectangle(0, 0, width - 20, 50);
        exit.Text = "Exit";
        exit.Click += () => GlobalVariables.Game.Exit();

        MenuElements.Add(windowSize);
        MenuElements.Add(fullscreen);
        MenuElements.Add(exit);
    }
}
