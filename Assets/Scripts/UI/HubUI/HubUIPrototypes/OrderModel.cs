using System;
using UnityEngine;

namespace BeastHunterHubUI
{
    public class OrderModel
    {
        private HubUIEventModel _orderEvent;

        public Action<string> OnCompleteHandler { get; set; }

        public OrderType OrderType { get; private set; }
        public int BaseSpentHours { get; private set; }
        public CharacterModel CharacterAssigned { get; private set; }
        public GameTimeStruct? CompletionTime { get; private set; }


        public OrderModel(OrderType orderType, int baseSpentHours)
        {
            _orderEvent = null;
            OrderType = orderType;
            BaseSpentHours = baseSpentHours;
            CharacterAssigned = null;
            CompletionTime = null;
        }

        
        public void AssignCharacter(CharacterModel character)
        {
            character.IsHaveOrder = true;
            CharacterAssigned = character;
            CompletionTime = HubUIServices.SharedInstance.OrdersService.CalculateOrderCompletionTime(character, this);
            _orderEvent = HubUIServices.SharedInstance.EventsService.CreateNewOrderEvent(this);
            _orderEvent.OnInvokeHandler = Complete;

            HubUIServices.SharedInstance.GameMessages.Notice
                ($"Character {character.Name} get the order {OrderType}. Completion time: {CompletionTime}");
        }

        public void RemoveAssignedCharacter()
        {
            if (CharacterAssigned != null)
            {
                CharacterAssigned.IsHaveOrder = false;
                HubUIServices.SharedInstance.EventsService.RemoveOrderEvent(CompletionTime.Value, _orderEvent);
                CharacterAssigned = null;
                CompletionTime = null;
                _orderEvent = null;
            }
        }

        public void Complete()
        {
            if (CharacterAssigned != null)
            {
                CharacterAssigned.IsHaveOrder = false;
                CharacterAssigned = null;
                CompletionTime = null;
                _orderEvent = null;
            }
            OnCompleteHandler?.Invoke($"The order {OrderType} is completed!");
        }
    }
}
