using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    class CitizenUIBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject _citizenNamePanel;
        [SerializeField] private GameObject _citizenPortrait;
        [SerializeField] private GameObject _exclamationImg;

        public Action<int> OnClick_CitizenButtonHandler;

        public void Initialize(ICitizenInfo citizen)
        {
            _citizenNamePanel.GetComponent<Text>().text = citizen.Name;
            _citizenPortrait.GetComponent<Image>().sprite = citizen.Portrait;
            _exclamationImg.SetActive(citizen.IsHaveQuest);
            GetComponent<Button>().onClick.AddListener(() => OnClick_CitizenButton(citizen.Id));
        }

        public void OnClick_CitizenButton(int citizenId)
        {
            Debug.Log("OnClick_CitizenButton");
            OnClick_CitizenButtonHandler?.Invoke(citizenId);
        }
    }
}
