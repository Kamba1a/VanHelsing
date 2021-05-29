using UnityEngine;
using UnityEngine.UI;


namespace BeastHunterHubUI
{
    class WorkRoomCharacterListItemBehaviour : BaseSlotBehaviour<CharacterStorageType>
    {
        [SerializeField] Text _nameText;
        [SerializeField] Text _rankText;


        public void FillSlot(CharacterModel character)
        {
            base.FillSlot(character.Portrait);
            _nameText.text = character.Name;
            _rankText.text = character.Rank.ToString();
        }
    }
}
