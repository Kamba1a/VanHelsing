using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "BoulderData", menuName = "CreateData/SimpleInteractiveObjects/BoulderData", order = 0)]
    public sealed class BoulderData : SimpleInteractiveObjectData
    {
        #region Fields

        [SerializeField] private Vector3 _prefabPosition;

        #endregion


        #region Properties

        public Vector3 PrefabPosition => _prefabPosition;

        #endregion


        #region ClassLifeCycle

        public BoulderData()
        {
            _prefabPosition = new Vector3(514.99f, 14.172f, 764.55f);
        }

        #endregion


        #region Methods

        public override void MakeInteractive(BaseInteractiveObjectModel interactiveObjectModel, 
            ITrigger interactiveTrigger, Collider enteredCollider)
        {
            (interactiveObjectModel as BoulderModel).CanvasObject.gameObject.SetActive(true);
            interactiveObjectModel.IsInteractive = true;
        }

        public override void MakeNotInteractive(BaseInteractiveObjectModel interactiveObjectModel, 
            ITrigger interactiveTrigger, Collider exitedCollider)
        {
            interactiveObjectModel.IsInteractive = false;
            (interactiveObjectModel as BoulderModel).CanvasObject.gameObject.SetActive(false);
        }

        protected override void Activate(SimpleInteractiveObjectModel interactiveObjectModel)
        {
            //addforce..
        }

        protected override void Deactivate(SimpleInteractiveObjectModel interactiveObjectModel)
        {
            //nothing
        }

        #endregion
    }
}

