using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    class CitizenInfoBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject _citizenNamePanel;
        [SerializeField] private GameObject _exclamationImg;
        private ICitizenInfo _citizen;

        public void Initialize(ICitizenInfo citizen)
        {
            _citizen = citizen;
            _citizenNamePanel.GetComponent<Text>().text = citizen.Name;
            _exclamationImg.SetActive(citizen.IsHaveQuest);
        }

        public void OnClick_CitizenButton()
        {
            Debug.Log("Open dialog window");
        }
    }
}
