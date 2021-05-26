using System;
using System.Collections.Generic;
using UnityEngine;


namespace BeastHunterHubUI
{
    public class PlayerModel
    {
        #region Properties

        public Action<int> OnChangeGoldAmount { get; set; }

        public int GoldAmount { get; private set; }
        public int Rank { get; private set; }
        public ItemLimitedStorage Inventory { get; private set; }
        public CharacterUnlimitedStorage AvailableCharacters { get; private set; }
        public HuntingQuestModel HuntingQuest { get; private set; } 

        #endregion


        #region ClassLifeCycle

        public PlayerModel(PlayerSettingsStruct settings, AllCharactersData allCharactersData)
        {
            GoldAmount = settings.GoldAmount;
            Rank = settings.StartingRank;

            AvailableCharacters = new CharacterUnlimitedStorage(CharacterStorageType.AvailableCharacters);

            //todo: uncomment?
            //for (int i = 0; i < settings.StartHiredCharacters.Length; i++)
            //{
            //    HiredCharacters.Add(new CharacterModel(settings.StartHiredCharacters[i]));
            //}

            Inventory = new ItemLimitedStorage(settings.InventorySlotsAmount, ItemStorageType.GeneralInventory);
            for (int i = 0; i < settings.StartInventoryItems.Length; i++)
            {
                BaseItemModel itemModel = HubUIServices.SharedInstance.
                    ItemInitializeService.InitializeItemModel(settings.StartInventoryItems[i]);
                Inventory.PutElement(i, itemModel);
            }
        }

        #endregion


        #region Methods

        public void TakeHuntingQuest(HuntingQuestModel quest)
        {
            if (HuntingQuest == null)
            {
                HuntingQuest = quest;
                HuntingQuest.OnCompletedHandler += DropHuntingQuest;
                quest.Activate();
            }
            else
            {
                Debug.LogError("The new hunting quest cannot be taken because the player already has a hunting quest");
            }
        }

        public void DropHuntingQuest()
        {
            HuntingQuest.OnCompletedHandler -= DropHuntingQuest;
            HuntingQuest = null;
        }

        public void HireCharacter(CharacterModel character)
        {
            AvailableCharacters.PutElementToFirstEmptySlot(character);
        }

        public void FireCharacter(CharacterModel character)
        {
            AvailableCharacters.RemoveElement(character);
        }

        public bool AddGold(int goldAmount)
        {
            if (goldAmount > 0)
            {
                GoldAmount += goldAmount;
                OnChangeGoldAmount?.Invoke(GoldAmount);
                return true;
            }
            Debug.LogError("goldAmount parameter is less than or equal to zero");
            return false;
        }

        public bool TakeGold(int goldAmount)
        {
            if (goldAmount > 0)
            {
                GoldAmount -= goldAmount;
                OnChangeGoldAmount?.Invoke(GoldAmount);
                return true;
            }
            Debug.LogError("goldAmount parameter is less than or equal to zero");
            return false;
        }

        #endregion
    }
}
