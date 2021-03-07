using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    public class CharacterUIBehaviour: MonoBehaviour
    {
        #region Properties

        public Action<int[]> OnClick_CharacterButtonHandler { get; set; }

        #endregion


        #region Methods

        public void Initialize(ICharacter character)
        {
            GetComponent<Image>().sprite = character.Portrait;
            GetComponent<Button>().onClick.AddListener(() => OnClick_CharacterButton(character.ItemsId));
        }

        public void OnClick_CharacterButton(int[] items)
        {
            OnClick_CharacterButtonHandler?.Invoke(items);
        }

        #endregion
    }
}