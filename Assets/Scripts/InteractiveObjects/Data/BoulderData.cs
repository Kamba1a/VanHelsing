using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "BoulderData", menuName = "CreateData/SimpleInteractiveObjects/BoulderData", order = 0)]
    public sealed class BoulderData : SimpleInteractiveObjectData
    {
        #region Fields

        [SerializeField] private Vector3 _prefabPosition;
        [SerializeField] private Vector3 _prefabEulers;
        [SerializeField] private float _pushingForce;

        [Header("Rigidbody")]
        [SerializeField] private float _mass;
        [SerializeField] private float _drag;
        [SerializeField] private float _angularDrag;
        [SerializeField] private float _bounciness;

        #endregion


        #region Properties

        public Vector3 PrefabPosition => _prefabPosition ;
        public Vector3 PrefabEulers => _prefabEulers;
        public float PushingForce => _pushingForce;
        public float Mass => _mass;
        public float Drag => _drag;
        public float AngularDrag => _angularDrag;
        public float Bounciness => _bounciness;

        #endregion


        #region ClassLifeCycle

        public BoulderData()
        {
            _prefabPosition = new Vector3(514.99f, 14.172f, 764.55f);
            _prefabEulers = new Vector3();

            _pushingForce = 100.0f;
            _mass = 40.0f;
            _drag = 0.1f;
            _angularDrag = 0.75f;
            _bounciness = 0.1f;
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
            Vector3 force = new Vector3(0,0,PushingForce);
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

