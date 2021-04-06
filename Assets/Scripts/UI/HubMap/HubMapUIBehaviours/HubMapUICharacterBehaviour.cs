using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    public class HubMapUICharacterBehaviour : MonoBehaviour
    {
        [SerializeField] Image _selectFrameImage;

        public Action<HubMapUICharacterModel> OnClick_ButtonHandler;

        public void FillInfo(HubMapUICharacterModel character)
        {
            _selectFrameImage.enabled = false;
            GetComponent<Image>().sprite = character.Portrait;
            GetComponent<Button>().onClick.AddListener(() => OnClick_Button(character));
        }

        public void SelectFrameSwitch(bool flag)
        {
            _selectFrameImage.enabled = flag;
        }

        private void OnClick_Button(HubMapUICharacterModel character)
        {
            OnClick_ButtonHandler?.Invoke(character);
        }
    }
}
