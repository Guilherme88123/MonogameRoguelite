using Application.Interface.Camera;
using Application.Interface.Room;
using Application.Service.Camera;
using Microsoft.Extensions.DependencyInjection;
using MonogameRoguelite.Dto;
using MonogameRoguelite.Service.Room;

var services = new ServiceCollection();

services.AddSingleton<IMapService, MapService>();
services.AddSingleton<ICameraService, CameraService>();

var provider = services.BuildServiceProvider();

GlobalVariables.ServiceProvider = provider;

using var game = new MonogameRoguelite.Roguelite();
game.Run();
