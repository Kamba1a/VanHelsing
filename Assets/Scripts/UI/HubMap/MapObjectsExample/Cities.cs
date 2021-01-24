using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeastHunter
{
    public class Cities
    {
        public List<CityModel> CitiesList;

        public Cities()
        {
            CitiesList = new List<CityModel>();
            ItemsForSale itemsForSale = new ItemsForSale();
            QuestGivers questGivers = new QuestGivers();

            CitiesList.Add(new CityModel()
            {
                Id = 1,
                Name = "AAA",
                Description = "AAA",
                ItemsForSale = itemsForSale.ItemsForSaleList,
                QuestGivers = questGivers.QuestGiversList,
                Reputation = 15.0f
            });
        }
    }
}
