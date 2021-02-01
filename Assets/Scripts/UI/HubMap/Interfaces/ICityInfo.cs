using System.Collections.Generic;
using UnityEngine.UI;

namespace BeastHunter
{
    public interface ICityInfo
    {
        public string Name { get; }
        public string Description { get; }
        public Image Fraction { get; }
        public List<ICitizenInfo> Citizens { get; }
        public float Reputation { get; }
        //public List<ItemForSaleModel> ItemsForSale { get; }
    }
}
