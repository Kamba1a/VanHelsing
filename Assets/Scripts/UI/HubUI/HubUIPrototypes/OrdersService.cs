using System.Collections.Generic;
using UnityEngine;

namespace BeastHunterHubUI
{
    public class OrdersService
    {
        private HubUIContext _context;


        public OrdersService(HubUIContext context)
        {
            _context = context;
        }


        public GameTimeStruct CalculateOrderCompletionTime(CharacterModel character, OrderModel order)
        {
            GameTimeStruct orderCompletionTime = _context.GameTime.AddTime(order.BaseSpentHours);
            //
            //calculate logic
            //
            return orderCompletionTime;
        }
    }
}
