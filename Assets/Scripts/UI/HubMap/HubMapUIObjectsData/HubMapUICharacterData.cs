using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUICharacter", menuName = "CreateData/HubMapUIData/HubMapUICharacter", order = 0)]
    public class HubMapUICharacterData : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _name;
        [SerializeField] private Sprite _portrait;
        [SerializeField] private bool _isFemale;
        [SerializeField] private GameObject _view3DModelPrefab;
        [SerializeField] private RuntimeAnimatorController _view3DModelAnimatorController;
        [SerializeField] private HubMapUIBaseItemData[] _startBackpuckItems;
        [SerializeField] private HubMapUIBaseItemData[] _startEquipmentItems;

        #endregion


        #region Properties

        public string Name => _name;
        public Sprite Portrait => _portrait;
        public bool IsFemale => _isFemale;
        public GameObject View3DModelPrefab => _view3DModelPrefab;
        public RuntimeAnimatorController View3DModelAnimatorController => _view3DModelAnimatorController;
        public HubMapUIBaseItemData[] StartBackpuckItems => (HubMapUIBaseItemData[])_startBackpuckItems.Clone();
        public HubMapUIBaseItemData[] StartEquipmentItems => (HubMapUIBaseItemData[])_startEquipmentItems.Clone();

        #endregion
    }
}
