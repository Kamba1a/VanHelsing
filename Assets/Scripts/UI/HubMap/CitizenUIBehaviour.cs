using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    class CitizenUIBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject _citizenNamePanel;
        [SerializeField] private GameObject _citizenPortrait;
        [SerializeField] private GameObject _exclamationImg;

        public void Initialize(ICitizenInfo citizen)
        {
            _citizenNamePanel.GetComponent<Text>().text = citizen.Name;
            _citizenPortrait.GetComponent<Image>().sprite = citizen.Portrait;
            _exclamationImg.SetActive(citizen.IsHaveQuest);
        }

        public void OnClick_CitizenButton()
        {
            Debug.Log("Open dialog window");
        }
    }
}
