using System;
using System.Collections.Generic;
using UnityEngine;


namespace BeastHunterHubUI
{
    public class WorkRoomModel : BaseWorkRoomModel<WorkRoomProgress>
    {
        #region Properties

        public Action<ItemOrderModel> OnOrderCompleteHandler { get; set; }

        public OrderLimitedStorage OrdersSlots { get; private set; }
        public override Dictionary<int, WorkRoomProgress> ProgressScheme { get; protected set; }

        #endregion


        #region ClassLifeCycle

        public WorkRoomModel(WorkRoomStruct roomStruct) : base(roomStruct.BaseWorkRoomStruct)
        {
            OrdersSlots = new OrderLimitedStorage(ProgressScheme[Level].OrderSlots);

            OrdersSlots.OnPutElementToSlotHandler += OnOrderAdd;
            OrdersSlots.OnTakeElementFromSlotHandler += OnOrderRemove;
        }

        #endregion


        #region Methods

        protected override void RoomImprove()
        {
            OrdersSlots.AddSlots(ProgressScheme[Level].OrderSlots - OrdersSlots.GetSlotsCount());
        }

        protected override float CountOrderTimeReducePercent()
        {
            //todo: counting by design document
            //temporary for debug:
            float maxLevelSkill = 200.0f;
            float maxReducePercent = 0.5f;
            float totalLevelSkill = AssistansGeneralSkillLevel;
            if (ChiefWorkplace.GetElementBySlot(0) != null)
            {
                totalLevelSkill += ChiefWorkplace.GetElementBySlot(0).TemporarySkillLevelForDebug;
            }
            return 1 - (totalLevelSkill * maxReducePercent / maxLevelSkill);
        }

        protected override void OnChiefAdd(CharacterModel character)
        {
            for (int i = 0; i < OrdersSlots.GetSlotsCount(); i++)
            {
                ItemOrderModel order = OrdersSlots.GetElementBySlot(i);
                if (order != null)
                {
                    order.UpdateHoursNumberToComplete(OrderTimeReducePercent);
                    order.AddOrderEvent();
                }
            }
        }

        protected override void OnChiefRemove(CharacterModel character)
        {
            for (int i = 0; i < OrdersSlots.GetSlotsCount(); i++)
            {
                ItemOrderModel order = OrdersSlots.GetElementBySlot(i);
                if (order != null)
                {
                    order.UpdateHoursNumberToComplete(OrderTimeReducePercent);
                    order.RemoveOrderEvent();
                }
            }
        }

        protected override void OnAssistantAdd(CharacterModel character)
        {
            for (int i = 0; i < OrdersSlots.GetSlotsCount(); i++)
            {
                ItemOrderModel order = OrdersSlots.GetElementBySlot(i);
                if (order != null)
                {
                    order.UpdateHoursNumberToComplete(OrderTimeReducePercent);
                }
            }
        }

        protected override void OnAssistantRemove(CharacterModel character)
        {
            for (int i = 0; i < OrdersSlots.GetSlotsCount(); i++)
            {
                ItemOrderModel order = OrdersSlots.GetElementBySlot(i);
                if (order != null)
                {
                    order.UpdateHoursNumberToComplete(OrderTimeReducePercent);
                }
            }
        }

        private void OnOrderAdd(OrderStorageType storageType, int slotIndex, ItemOrderModel order)
        {
            if (ChiefWorkplace.GetElementBySlot(0) != null)
            {
                order.OnCompleteHandler += OnOrderComplete;
                order.AddOrderEvent();
            }
        }

        private void OnOrderRemove(OrderStorageType storageType, int slotIndex, ItemOrderModel order)
        {
            order.OnCompleteHandler = null;
            order.RemoveOrderEvent();
        }

        private void OnOrderComplete(ItemOrderModel order)
        {
            OnOrderCompleteHandler?.Invoke(order);
        }

        #endregion
    }
}
