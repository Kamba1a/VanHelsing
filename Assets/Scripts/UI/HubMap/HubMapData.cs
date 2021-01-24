using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapData", menuName = "CreateData/HubMapData", order = 0)]
    public class HubMapData: ScriptableObject
    {
        #region SerializeFields

        [SerializeField] private bool _mapOnStartEnabled;
        [SerializeField] private string _mainPanelName;
        [SerializeField] private string _hubButtonName;
        [SerializeField] private string _mapButtonName;

        #endregion


        #region Properties

        public bool MapOnStartEnabled => _mapOnStartEnabled;
        public string MainPanelName => _mainPanelName;

        #endregion


        #region Methods

        public void HubButton_OnClick(GameObject button, HubMapModel model)
        {
            if (button.name == _hubButtonName)
            {
                model.MainPanel.SetActive(false);
            }
            else if (button.name == _mapButtonName)
            {
                model.MainPanel.SetActive(true);
            }
        }

        public void Updating()
        {
            
        }

        #endregion
    }
}
