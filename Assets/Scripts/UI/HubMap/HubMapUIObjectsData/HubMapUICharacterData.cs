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
        [InspectorName("Start backpuck items")]
        [SerializeField] private HubMapUIBaseItemData[] _startItems; //backpuck items
        [SerializeField] private HubMapUIBaseItemData[] _startEquipmentItems;

        #endregion


        #region Properties

        public string Name => _name;
        public Sprite Portrait => _portrait;
        public GameObject View3DModelPrefab => _view3DModelPrefab;
        public RuntimeAnimatorController View3DModelAnimatorController => _view3DModelAnimatorController;
        public HubMapUIBaseItemData[] StartBackpuckItems => (HubMapUIBaseItemData[])_startItems.Clone();
        public HubMapUIBaseItemData[] StartEquipmentItems => (HubMapUIBaseItemData[])_startEquipmentItems.Clone();

        #endregion
    }
}
