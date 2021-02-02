using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{
    public interface ICityInfo
    {
        public string Name { get; }
        public string Description { get; }
        public Sprite Fraction { get; }
        public List<ICitizenInfo> Citizens { get; }
        public float Reputation { get; }
        public List<ISellingItem> SellingItems { get; }
    }
}
