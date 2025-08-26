using Application.Enum;
using System;
using System.Collections.Generic;

namespace Application.Infrastructure;

public static class RngHelper
{
    private static readonly Random Rng = new Random();
    private static readonly Dictionary<RarityType, double> RarityChances = new()
    {
        { RarityType.Common,    50.0 },  // 50%
        { RarityType.Uncommon,  25.0 },  // 25%
        { RarityType.Rare,      15.0 },  // 15%
        { RarityType.Epic,       7.0 },  // 7%
        { RarityType.Legendary,  2.0 },  // 2%
        { RarityType.Mythic,     0.5 },  // 0.5%
        { RarityType.Secret,     0.01 }   // 0.01%
    };

    /// <summary>
    /// Sorteia uma raridade baseada nas chances configuradas.
    /// </summary>
    public static RarityType GetRandomRarity()
    {
        double roll = Rng.NextDouble() * 100.0; // valor entre 0 e 100
        double cumulative = 0.0;

        foreach (var kvp in RarityChances)
        {
            cumulative += kvp.Value;
            if (roll <= cumulative)
                return kvp.Key;
        }

        return RarityType.Common; // fallback (não deveria acontecer se chances somarem 100)
    }

    /// <summary>
    /// Obtém a chance de uma raridade específica (%).
    /// </summary>
    public static double GetChance(RarityType rarity)
    {
        return RarityChances.TryGetValue(rarity, out var chance) ? chance : 0.0;
    }
}
