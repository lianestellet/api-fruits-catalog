﻿using Entities.Domain;

namespace Utils.Fixtures
{
    public static class TropicalFixture
    {
        public static readonly FruitType Type = new("Tropical")
        {
            Id = 5,
            Description = "Known for their exotic flavors and vibrant colors"
        };

        public static readonly Fruit Pineapple = new("Pineapple")
        {
            Id = 41,
            Description = "Tropical and tangy",
            FruitTypeId = Type.Id
        };

        public static readonly Fruit Mango = new("Mango")
        {
            Id = 42,
            Description = "Sweet and juicy",
            FruitTypeId = Type.Id
        };

        public static readonly Fruit Papaya = new("Papaya")
        {
            Id = 43,
            Description = "Soft and sweet",
            FruitTypeId = Type.Id
        };

        public static readonly Fruit Coconut = new("Coconut")
        {
            Id = 44,
            Description = "Rich and creamy",
            FruitTypeId = Type.Id
        };

        public static readonly List<Fruit> AllTropicalFruits = [Pineapple, Mango, Papaya, Coconut ];
    }
}
