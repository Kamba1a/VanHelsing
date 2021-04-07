using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUICharacter", menuName = "CreateData/HubMapUIData/HubMapUICharacter", order = 0)]
    public class HubMapUICharacterData : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _name;
        [SerializeField] private Sprite _portrait;
        [SerializeField] private GameObject _view3DModelPrefab;
        [SerializeField] private RuntimeAnimatorController _view3DModelAnimatorController;
        [SerializeField] private BaseItem[] _startItems;

        #endregion


        #region Properties

        public string Name => _name;
        public Sprite Portrait => _portrait;
        public GameObject View3DModelPrefab => _view3DModelPrefab;
        public RuntimeAnimatorController View3DModelAnimatorController => _view3DModelAnimatorController;
        public BaseItem[] StartItems => (BaseItem[])_startItems.Clone();

        #endregion
    }
}
