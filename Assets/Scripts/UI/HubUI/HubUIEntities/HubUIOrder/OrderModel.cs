using System;


namespace BeastHunterHubUI
{
    public class OrderModel
    {
        #region Fields

        private HubUIEventModel _orderEvent;

        #endregion


        #region Properties

        public Action<OrderModel> OnCompleteHandler { get; set; }

        public OrderType OrderType { get; private set; }
        public int BaseSpentHours { get; private set; }
        public CharacterModel CharacterAssigned { get; private set; }

        #endregion


        #region ClassLifeCycle

        public OrderModel(OrderType orderType, int baseSpentHours)
        {
            _orderEvent = null;
            OrderType = orderType;
            BaseSpentHours = baseSpentHours;
            CharacterAssigned = null;
        }

        #endregion


        #region Methods

        public void AssignCharacter(CharacterModel character)
        {
            character.IsHaveOrder = true;
            CharacterAssigned = character;

            string eventMsg = $"The order {OrderType} is completed";
            _orderEvent = new HubUIEventModel(BaseSpentHours, eventMsg);
            _orderEvent.OnInvokeHandler = Complete;

            HubUIServices.SharedInstance.EventsService.AddEventToScheduler(_orderEvent);
            HubUIServices.SharedInstance.GameMessages.Notice
                ($"Character {character.Name} get the order {OrderType}. Completion time: {_orderEvent.InvokeTime}");
        }

        public void RemoveAssignedCharacter()
        {
            if (CharacterAssigned != null)
            {
                CharacterAssigned.IsHaveOrder = false;
                HubUIServices.SharedInstance.EventsService.RemoveEventFromScheduler(_orderEvent);
                CharacterAssigned = null;
                _orderEvent = null;
            }
        }

        public void Complete()
        {
            if (CharacterAssigned != null)
            {
                CharacterAssigned.IsHaveOrder = false;
                CharacterAssigned = null;
                _orderEvent = null;
            }
            OnCompleteHandler?.Invoke(this);
        }

        #endregion
    }
}
