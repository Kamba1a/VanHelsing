using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    class CitizenBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject _citizenNamePanel;
        [SerializeField] private GameObject _exclamationImg;

        public void Initialize(TemporaryCitizenModel citizen)
        {
            _citizenNamePanel.GetComponent<Text>().text = citizen.Name;

            if (citizen.IsHaveQuest)
            {
                _exclamationImg.SetActive(true);
            }
        }

        public void OnClick_CitizenButton()
        {
            Debug.Log("Open dialog window");
        }
    }
}
