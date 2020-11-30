using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "BoulderData", menuName = "CreateData/SimpleInteractiveObjects/BoulderData", order = 0)]
    public sealed class BoulderData : SimpleInteractiveObjectData
    {
        #region Fields

        [SerializeField] private Vector3 _prefabPosition;
        [SerializeField] private Vector3 _prefabEulers;

        #endregion


        #region Properties

        public Vector3 PrefabPosition => _prefabPosition;
        public Vector3 PrefabEulers => _prefabEulers;

        #endregion


        #region ClassLifeCycle

        public BoulderData()
        {
            _prefabPosition = new Vector3(514.99f, 14.172f, 764.55f);
            _prefabEulers = new Vector3();
        }

        #endregion


        #region Methods

        //shows the canvas when entering the boulder trigger
        public override void MakeInteractive(BaseInteractiveObjectModel interactiveObjectModel, 
            ITrigger interactiveTrigger, Collider enteredCollider)
        {
            (interactiveObjectModel as BoulderModel).CanvasObject.gameObject.SetActive(true);
            interactiveObjectModel.IsInteractive = true;
        }

        //hides the canvas when exiting the boulder trigger
        public override void MakeNotInteractive(BaseInteractiveObjectModel interactiveObjectModel, 
            ITrigger interactiveTrigger, Collider exitedCollider)
        {
            interactiveObjectModel.IsInteractive = false;
            (interactiveObjectModel as BoulderModel).CanvasObject.gameObject.SetActive(false);
        }

        //what happens when activated
        protected override void Activate(SimpleInteractiveObjectModel interactiveObjectModel)
        {
            BoulderModel model = interactiveObjectModel as BoulderModel;

            model.Rigidbody.constraints = RigidbodyConstraints.None;
            Vector3 force = new Vector3(0,0,100);
            model.Rigidbody.AddRelativeForce(force, ForceMode.Impulse);
        }

        //what happens when deactivated
        protected override void Deactivate(SimpleInteractiveObjectModel interactiveObjectModel)
        {
            //deactivation should happen automatically, not by the player
        }

        #endregion
    }
}

