using UnityEngine;


namespace BeastHunter
{
    public class HubMapUICharacterModel
    {
        #region Fields

        private GameObject _view3DModelPrefab;
        private RuntimeAnimatorController _view3DModelAnimatorController;

        #endregion


        #region Properties

        public string Name { get; private set; }
        public Sprite Portrait { get; private set; }
        public GameObject View3DModelObjectOnScene { get; private set; }
        public HubMapUIStorage Backpack { get; private set; }

        //todo:
        public HubMapUIClothEquipment ClothEquipment { get; private set; }
        //public HubMapUIEquipmentModel Pockets { get; private set; }
        //public HubMapUIEquipmentModel Weapon { get; private set; }

        public HubMapUICharacterBehaviour Behaviour { get; set; }

        #endregion


        #region ClassLifeCycle

        public HubMapUICharacterModel(HubMapUICharacterData data, int backpackSize, HubMapUIClothType[] clothEquipment)
        {
            Name = data.Name;
            Portrait = data.Portrait;
            _view3DModelPrefab = data.View3DModelPrefab;
            _view3DModelAnimatorController = data.View3DModelAnimatorController;

            Backpack = new HubMapUIStorage(backpackSize);
            for (int i = 0; i < data.StartBackpuckItems.Length; i++)
            {
                HubMapUIBaseItemModel itemModel = HubMapUIServices.SharedInstance.ItemInitializeService.InitializeItemModel(data.StartBackpuckItems[i]);
                Backpack.PutItem(i, itemModel);
            }

            ClothEquipment = new HubMapUIClothEquipment(clothEquipment);
        }

        #endregion


        #region Methods

        public void InitializeView3DModel(Transform parent)
        {
            View3DModelObjectOnScene = GameObject.Instantiate(_view3DModelPrefab, parent);
            View3DModelObjectOnScene.GetComponent<Animator>().runtimeAnimatorController = _view3DModelAnimatorController;
            View3DModelObjectOnScene.SetActive(false);
        }

        #endregion
    }
}
