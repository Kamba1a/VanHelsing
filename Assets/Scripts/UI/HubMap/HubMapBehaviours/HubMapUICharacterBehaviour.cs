using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    public class HubMapUICharacterBehaviour: MonoBehaviour
    {
        #region Properties

        public Action<HubMapUICharacter> OnClick_CharacterButtonHandler { get; set; }

        #endregion


        #region Methods

        public void FillCharacterInfo(HubMapUICharacter character)
        {
            GetComponent<Image>().sprite = character.Portrait;
            GetComponent<Button>().onClick.AddListener(() => OnClick_CharacterButton(character));
        }

        public void OnClick_CharacterButton(HubMapUICharacter character)
        {
            OnClick_CharacterButtonHandler?.Invoke(character);
        }

        #endregion
    }
}