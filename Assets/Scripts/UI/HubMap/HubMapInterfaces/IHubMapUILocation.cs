﻿using UnityEngine;

namespace BeastHunter
{
    public interface IHubMapUILocation
    {
        public string Name { get; }
        public string Description { get; }
        public Sprite Screenshot { get; }
        public int[] DwellersId { get; }
        public int[] IngredientsId { get; }
    }
}