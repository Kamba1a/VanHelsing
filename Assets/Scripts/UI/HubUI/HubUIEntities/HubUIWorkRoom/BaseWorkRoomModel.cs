using System;
using System.Collections.Generic;
using UnityEngine;


namespace BeastHunterHubUI
{
    public abstract class BaseWorkRoomModel<T> where T : BaseWorkRoomProgress
    {
        #region Properties

        public Action<WorkRoomType> OnLevelUpHandler { get; set; }
        public Action OnInfoUpdateHandler { get; set; }

        public WorkRoomType RoomType { get; private set; }
        public SkillType UsedSkill { get; private set; }
        public string Name { get; private set; }
        public int Level { get; private set; }
        public CharacterLimitedStorage ChiefWorkplace { get; private set; }
        public CharacterLimitedStorage AssistantWorkplaces { get; private set; }
        public abstract Dictionary<int,T> ProgressScheme { get; protected set; }
        public float OrderTimeReducePercent { get; private set; }
        public int AssistansGeneralSkillLevel { get; private set; }

        #endregion


        #region ClassLifeCycle

        public BaseWorkRoomModel(BaseWorkRoomStruct<T> roomStruct)
        {
            RoomType = roomStruct.RoomType;
            UsedSkill = roomStruct.UsedSkill;
            Name = roomStruct.Name;
            OrderTimeReducePercent = 1;

            ProgressScheme = new Dictionary<int, T>();
            for (int i = 0; i < roomStruct.ProgressScheme.Count; i++)
            {
                if (!ProgressScheme.ContainsKey(roomStruct.ProgressScheme[i].Level))
                {
                    ProgressScheme.Add(roomStruct.ProgressScheme[i].Level, roomStruct.ProgressScheme[i].Progress);
                }
                else
                {
                    Debug.LogError("Incorrect input parameter: progress scheme list contain two identical values of level number");
                }
            }

            if (!ProgressScheme.ContainsKey(roomStruct.Level))
            {
                Debug.LogError($"Incorrect input data: the room level {roomStruct.Level} is not contained in the progress schema");
            }
            Level = roomStruct.Level;

            ChiefWorkplace = new CharacterLimitedStorage(1, CharacterStorageType.ChiefWorkplace);
            AssistantWorkplaces = new CharacterLimitedStorage(ProgressScheme[Level].AssistansAmount, CharacterStorageType.AssistantWorkplaces);

            ChiefWorkplace.OnPutElementToSlotHandler += OnChiefAddBase;
            ChiefWorkplace.OnTakeElementFromSlotHandler += OnChiefRemoveBase;
            if (roomStruct.ChiefWorker != null)
            {
                ChiefWorkplace.PutElementToFirstEmptySlot(roomStruct.ChiefWorker);
            }

            AssistantWorkplaces.OnPutElementToSlotHandler += OnAssistantAddBase;
            AssistantWorkplaces.OnTakeElementFromSlotHandler += OnAssistantRemoveBase;
            if (roomStruct.Assistants != null)
            {
                for (int i = 0; i < roomStruct.Assistants.Count; i++)
                {
                    if (!AssistantWorkplaces.PutElementToFirstEmptySlot(roomStruct.Assistants[i]))
                    {
                        Debug.LogError("Incorrect input parameter: assistans amount more than the number of slots");
                        break;
                    }
                }
            }
        }

        #endregion


        #region Methods

        protected abstract void RoomImprove();
        protected abstract float CountOrderTimeReducePercent();
        protected abstract void OnChiefAdd(CharacterModel character);
        protected abstract void OnChiefRemove(CharacterModel character);
        protected abstract void OnAssistantAdd(CharacterModel character);
        protected abstract void OnAssistantRemove(CharacterModel character);

        public void LevelUp()
        {
            if (ProgressScheme.ContainsKey(Level + 1))
            {
                Level += 1;
                AssistantWorkplaces.AddSlots(ProgressScheme[Level].AssistansAmount - AssistantWorkplaces.GetSlotsCount());
                RoomImprove();
                OnLevelUp();
            }
        }

        private void OnChiefAddBase(CharacterStorageType storageType, int slotIndex, CharacterModel character)
        {
            character.IsAssignedToWork = true;
            RecountOrderTimeReducePercent();
            OnChiefAdd(character);
            OnInfoUpdateHandler?.Invoke();
        }

        private void OnChiefRemoveBase(CharacterStorageType storageType, int slotIndex, CharacterModel character)
        {
            character.IsAssignedToWork = false;
            RecountOrderTimeReducePercent();
            OnChiefRemove(character);
            OnInfoUpdateHandler?.Invoke();
        }

        private void OnAssistantAddBase(CharacterStorageType storageType, int slotIndex, CharacterModel character)
        {
            character.IsAssignedToWork = true;
            RecountAssistansGeneralSkillLevel();
            RecountOrderTimeReducePercent();
            OnAssistantAdd(character);
            OnInfoUpdateHandler?.Invoke();
        }

        private void OnAssistantRemoveBase(CharacterStorageType storageType, int slotIndex, CharacterModel character)
        {
            character.IsAssignedToWork = false;
            RecountAssistansGeneralSkillLevel();
            RecountOrderTimeReducePercent();
            OnAssistantRemove(character);
            OnInfoUpdateHandler?.Invoke();
        }

        private void RecountAssistansGeneralSkillLevel()
        {
            AssistansGeneralSkillLevel = 0;
            for (int i = 0; i < AssistantWorkplaces.GetSlotsCount(); i++)
            {
                CharacterModel assistant = AssistantWorkplaces.GetElementBySlot(i);
                if (assistant != null)
                {
                    AssistansGeneralSkillLevel = Mathf.Clamp(AssistansGeneralSkillLevel + assistant.Skills[UsedSkill], 0, 100);
                }
            }
        }

        private void OnLevelUp()
        {
            OnLevelUpHandler?.Invoke(RoomType);
        }

        private void RecountOrderTimeReducePercent()
        {
            OrderTimeReducePercent = CountOrderTimeReducePercent();
        }

        #endregion
    }
}
