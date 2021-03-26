using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUICitizen", menuName = "CreateData/HubMapUIData/HubMapUICitizen", order = 0)]
    public class HubMapUICitizenData : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _name;
        [SerializeField] private Sprite _portrait;
        [SerializeField] private int _firstDialogId;
 
        #endregion


        #region Properties

        public string Name => _name;
        public Sprite Portrait => _portrait;
        public int FirstDialogId => _firstDialogId;

        #endregion
    }
}
