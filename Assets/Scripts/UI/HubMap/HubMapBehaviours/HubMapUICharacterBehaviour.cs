using System;
using UnityEngine;
using UnityEngine.UI;


namespace BeastHunter
{
    public class HubMapUICharacterBehaviour: MonoBehaviour  //todo: delete class?
    {
        #region Properties

        public Action<HubMapUICharacterModel> OnClick_CharacterButtonHandler { get; set; }

        #endregion


        #region Methods

        public void FillCharacterInfo(HubMapUICharacterModel character)
        {
            GetComponent<Image>().sprite = character.Portrait;
            GetComponent<Button>().onClick.AddListener(() => OnClick_CharacterButton(character));
        }

        public void OnClick_CharacterButton(HubMapUICharacterModel character)
        {
            OnClick_CharacterButtonHandler?.Invoke(character);
        }

        #endregion
    }
}