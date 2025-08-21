using Microsoft.Extensions.DependencyInjection;
using Teste001.Dto;
using Teste001.Interface;
using Teste001.Service.Room;

var services = new ServiceCollection();

services.AddSingleton<IMapService, MapService>();

var provider = services.BuildServiceProvider();

GlobalVariables.ServiceProvider = provider;

using var game = new Teste001.Roguelite();
game.Run();
