using System.Collections.Generic;


namespace BeastHunter
{
    public class HubMapUICityModel
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public HubMapUIFractionData Fraction { get; private set; } 
        public int PlayerReputation { get; set; }
        public List<HubMapUICitizen> Citizens { get; private set; }
        public List<BaseItem> ShopItemsPool { get; private set; }
        public List<BaseItem> ShopItems { get; private set; }
        public List<BaseItem> BuyBackItemsStorage { get; private set; }


        public HubMapUICityModel(HubMapUICityData data)
        {

        }
    }
}
