using System.Collections.Generic;

namespace BeastHunter
{
    public class ItemsForSale
    {
        public List<ItemForSaleModel> ItemsForSaleList;

        public ItemsForSale()
        {
            ItemsForSaleList = new List<ItemForSaleModel>();
                
            ItemsForSaleList.Add(new ItemForSaleModel()
            {
                Id = 1,
                Name = "AAA",
                Cost = 20,
                RequiredReputation = 20
            });

            ItemsForSaleList.Add(new ItemForSaleModel()
            {
                Id = 1,
                Name = "BBB",
                Cost = 15,
                RequiredReputation = 15
            });

            ItemsForSaleList.Add(new ItemForSaleModel()
            {
                Id = 3,
                Name = "CCC",
                Cost = 10,
                RequiredReputation = 10
            });
        }
    }
}
