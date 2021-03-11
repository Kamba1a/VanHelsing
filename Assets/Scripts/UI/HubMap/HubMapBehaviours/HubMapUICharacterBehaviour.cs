using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    public class HubMapUICharacterBehaviour: MonoBehaviour
    {
        #region Properties

        public Action<IHubMapUICharacter> OnClick_CharacterButtonHandler { get; set; }

        #endregion


        #region Methods

        public void Initialize(IHubMapUICharacter character)
        {
            GetComponent<Image>().sprite = character.Portrait;
            GetComponent<Button>().onClick.AddListener(() => OnClick_CharacterButton(character));
        }

        public void OnClick_CharacterButton(IHubMapUICharacter character)
        {
            OnClick_CharacterButtonHandler?.Invoke(character);
        }

        #endregion
    }
}