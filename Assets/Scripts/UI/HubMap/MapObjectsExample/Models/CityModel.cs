using System.Collections.Generic;
using UnityEngine.UI;

namespace BeastHunter
{
    public class CityModel
    {
        public int Id;
        public string Name;
        public string Description;
        public Image Fraction;
        public List<QuestGiverModel> QuestGivers;
        public float Reputation;
        public List<ItemForSaleModel> ItemsForSale;
    }
}
