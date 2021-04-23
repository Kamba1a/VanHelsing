namespace BeastHunterHubUI
{
    public struct OrderStruct
    {
        public OrderType OrderType;
        public int BaseSpentHours;

        public OrderStruct(OrderType orderType, int baseSpentHours)
        {
            OrderType = orderType;
            BaseSpentHours = baseSpentHours;
        }
    }
}
