using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    class CitizenUIBehaviour : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _citizenNamePanel;
        [SerializeField] private GameObject _citizenPortrait;
        [SerializeField] private GameObject _exclamationImg;

        #endregion


        #region Properties

        public Action<int> OnClick_CitizenButtonHandler { get; set; }

        #endregion


        #region Methods

        public void Initialize(ICitizenInfo citizen)
        {
            _citizenNamePanel.GetComponent<Text>().text = citizen.Name;
            _citizenPortrait.GetComponent<Image>().sprite = citizen.Portrait;
            _exclamationImg.SetActive(Data.HubMapData.Dialogs[citizen.CurrentDialogId].IsQuest);
            GetComponent<Button>().onClick.AddListener(() => OnClick_CitizenButton(citizen.Id));
        }

        public void OnClick_CitizenButton(int citizenId)
        {
            OnClick_CitizenButtonHandler?.Invoke(citizenId);
        }

        #endregion
    }
}
