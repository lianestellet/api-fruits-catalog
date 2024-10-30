﻿namespace Entities.DTOs
{
    public class FruitInfoDTO
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public FruitTypeDTO? FruitType { get; set; }
    }
}