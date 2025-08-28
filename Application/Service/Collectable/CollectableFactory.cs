using Application.Enum;
using Application.Model.Entities.Collectable.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Application.Service.Collectable;

public class CollectableFactory
{
    private static List<BaseCollectableModel> Collectables = Assembly.GetExecutingAssembly()
        .GetTypes()
        .Where(t => t.IsSubclassOf(typeof(BaseCollectableModel)) && !t.IsAbstract)
        .Select(t => (BaseCollectableModel)Activator.CreateInstance(t, (0f, 0f)))
        .ToList();

    public static BaseCollectableModel GetRandomCollectable(RarityType rarity)
    {
        var rarityCollectables = Collectables.Where(c => c.Rarity == rarity).ToList();

        var idx = Random.Shared.Next(0, rarityCollectables.Count);

        return rarityCollectables[idx];
    }
}
