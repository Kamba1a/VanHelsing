using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapData", menuName = "CreateData/HubMapData", order = 0)]
    public class HubMapData: ScriptableObject
    {
        #region SerializeFields

        [SerializeField] private bool _mapOnStartEnabled;
        [SerializeField] private string _mainPanelName;
        [SerializeField] private string _infoPanelName;

        #endregion


        #region Properties

        public bool MapOnStartEnabled => _mapOnStartEnabled;
        public string MainPanelName => _mainPanelName;

        #endregion


        #region Methods

        public void Updating()
        {
            
        }

        public void HubButton_OnClick(GameObject mainPanel)
        {
            mainPanel.SetActive(false);
        }

        public void MapButton_OnClick(GameObject mainPanel)
        {
            mainPanel.SetActive(true);
        }

        #endregion
    }
}
