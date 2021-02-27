using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    public class CharacterUIBehaviour: MonoBehaviour
    {
        #region Properties

        public Action OnClick_CharacterButtonHandler { get; set; }

        #endregion


        #region Methods

        public void Initialize(ICharacter character)
        {
            GetComponent<Image>().sprite = character.Portrait;
            GetComponent<Button>().onClick.AddListener(() => OnClick_CharacterButton());
        }

        public void OnClick_CharacterButton()
        {
            OnClick_CharacterButtonHandler?.Invoke();
        }

        #endregion
    }
}