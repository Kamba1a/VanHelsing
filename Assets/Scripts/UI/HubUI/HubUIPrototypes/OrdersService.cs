namespace BeastHunterHubUI
{
    public class OrdersService
    {
        private GameTimeModel _gameTime;


        public OrdersService(GameTimeModel gameTimeModel)
        {
            _gameTime = gameTimeModel;
        }


        public GameTimeStruct CalculateOrderCompletionTime(CharacterModel character, OrderStruct order)
        {
            GameTimeStruct orderCompletionTime = _gameTime.AddTime(order.BaseSpentHours);
            //
            //calculate logic
            //
            return orderCompletionTime;
        }
    }
}
