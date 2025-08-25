using Application.Interface.Camera;
using Application.Interface.Menu;
using Application.Interface.Room;
using Application.Service.Camera;
using Application.Service.Menu;
using Microsoft.Extensions.DependencyInjection;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Service.Room;

var services = new ServiceCollection();

services.AddSingleton<IMapService, MapService>();
services.AddSingleton<ICameraService, CameraService>();
services.AddSingleton<IMenuService, MenuService>();

var provider = services.BuildServiceProvider();

GlobalVariables.ServiceProvider = provider;

GlobalVariables.Game = new MonogameRoguelite.Roguelite();
GlobalVariables.Game.Run();
