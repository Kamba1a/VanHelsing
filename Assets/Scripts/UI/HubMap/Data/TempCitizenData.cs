using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "NewCitizen", menuName = "HubMap/Citizen", order = 0)]
    public class TempCitizenData: ScriptableObject
    {
        #region Fields

        [SerializeField] private string _name;
        [SerializeField] private Sprite _portrait;
        [SerializeField] private TemporaryDialogModel[] _dialogs;

        #endregion


        #region Properties

        public string Name => _name;
        public Sprite Portrait => _portrait;
        public IHubMapDialog[] Dialogs => _dialogs;

        #endregion
    }
}
