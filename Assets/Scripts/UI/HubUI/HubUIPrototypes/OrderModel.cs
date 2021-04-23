namespace BeastHunterHubUI
{
    public class OrderModel
    {
        public OrderType OrderType { get; private set; }
        public GameTimeStruct CompletionTime { get; private set; }


        public OrderModel(OrderType orderType, GameTimeStruct completionTime)
        {
            OrderType = orderType;
            CompletionTime = completionTime;
        }
    }
}
