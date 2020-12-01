using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "BouldersData", menuName = "CreateData/SimpleInteractiveObjects/BouldersData", order = 0)]
    public sealed class BouldersData : SimpleInteractiveObjectData
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

        public BouldersData()
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
            (interactiveObjectModel as BouldersModel).CanvasObject.gameObject.SetActive(true);
            interactiveObjectModel.IsInteractive = true;
        }

        //hides the canvas when exiting the boulder trigger
        public override void MakeNotInteractive(BaseInteractiveObjectModel interactiveObjectModel, 
            ITrigger interactiveTrigger, Collider exitedCollider)
        {
            interactiveObjectModel.IsInteractive = false;
            (interactiveObjectModel as BouldersModel).CanvasObject.gameObject.SetActive(false);
        }

        //what happens when activated
        protected override void Activate(SimpleInteractiveObjectModel interactiveObjectModel)
        {
            BouldersModel model = interactiveObjectModel as BouldersModel;

            Vector3 force = new Vector3(0, 0, PushingForce);
            for (int i = 0; i < model.Rigidbodies.Length; i++)
            {
                model.Rigidbodies[i].constraints = RigidbodyConstraints.None;
                model.Rigidbodies[i].AddRelativeForce(force, ForceMode.Impulse);
            }

            model.InteractiveTrigger.enabled = false;
            model.CanvasObject.gameObject.SetActive(false);
        }

        //what happens when deactivated
        protected override void Deactivate(SimpleInteractiveObjectModel interactiveObjectModel)
        {
            //does not require implementation (the object is activated once)
        }

        public void Act(BouldersModel model)
        {
            for (int i = 0; i < model.Rigidbodies.Length; i++)
            {
                if (model.Rigidbodies[i].velocity == Vector3.zero)
                {
                    //destroy
                }
                //if collision..
            }

        }

        #endregion
    }
}

