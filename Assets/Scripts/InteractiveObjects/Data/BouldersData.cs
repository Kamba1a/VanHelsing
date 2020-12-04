using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "BouldersData", menuName = "CreateData/SimpleInteractiveObjects/BouldersData", order = 0)]
    public sealed class BouldersData : SimpleInteractiveObjectData
    {
        private const float CHECK_VELOCITY_TIME = 2.0f;

        #region Fields

        [SerializeField] private Vector3 _prefabPosition;
        [SerializeField] private Vector3 _prefabEulers;
        [SerializeField] private float _pushingForce;
        [SerializeField] private float _timeToDeactivate;
        [SerializeField] private float _timeToDestroyAfterHit;
        [SerializeField] private float _hitSpeed;

        [Header("Damage")]
        [SerializeField] private float _physicalDamage;
        [SerializeField] private float _stunProbability;

        [Header("Rigidbody")]
        [SerializeField] private float _mass;
        [SerializeField] private float _drag;
        [SerializeField] private float _angularDrag;
        [SerializeField] private float _bounciness;

        private float sqrHitSpeed;

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
            _timeToDeactivate = 30.0f;
            _timeToDestroyAfterHit = 1.0f;
            _hitSpeed = 2;
        }

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            sqrHitSpeed = _hitSpeed * _hitSpeed;
        }

        #endregion


        #region SimpleInteractiveObjectData

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

        public override void Interact(BaseInteractiveObjectModel interactiveObjectModel)
        {
            BouldersModel model = interactiveObjectModel as BouldersModel;

            if (!model.IsActivated)
            {
                model.IsActivated = true;
                Activate(model);
            }
        }

        //what happens when activated
        protected override void Activate(SimpleInteractiveObjectModel interactiveObjectModel)
        {
            Debug.Log("Boulders activate");

            BouldersModel model = interactiveObjectModel as BouldersModel;
            model.IsInteractive = false;
            Destroy(model.InteractiveTrigger);
            Destroy(model.CanvasObject.gameObject);
            model.Timer = _timeToDeactivate;

            for (int i = 0; i < model.Rigidbodies.Length; i++)
            {
                model.Rigidbodies[i].constraints = RigidbodyConstraints.None;
                model.Rigidbodies[i].AddForce(model.Prefab.transform.forward * PushingForce, ForceMode.Impulse);
            }

            for (int i = 0; i < model.BoulderBehaviours.Count; i++)
            { 
                model.BoulderBehaviours[i].OnFilterHandler += CollisionFilter;
                model.BoulderBehaviours[i].OnCollisionEnterHandler += CollisionEnter;
            }
        }

        //what happens when deactivated
        protected override void Deactivate(SimpleInteractiveObjectModel interactiveObjectModel)
        {
            Debug.Log("Boulders deactivate");
            BouldersModel model = interactiveObjectModel as BouldersModel;
            model.Clean();
        }

        #endregion


        #region BouldersModel

        public void Act(BouldersModel model)
        {
            if (_timeToDeactivate - model.Timer > CHECK_VELOCITY_TIME)
            {
                for (int i = 0; i < model.Rigidbodies.Length; i++)
                {
                    if (model.Rigidbodies[i] != null && model.Rigidbodies[i].velocity == Vector3.zero)
                    {
                        Debug.Log("Destroy boulder rigidbody (velocity = 0,0,0)");
                        Destroy(model.Rigidbodies[i]);
                    }
                }
            }

            model.Timer -= Time.deltaTime;
            if (model.Timer <= 0)
            {
                model.IsActivated = false;
                Deactivate(model);
            }
        }

        public bool CollisionFilter(Collider collider)
        {
            InteractableObjectBehavior objectBehavior = collider.GetComponentInChildren<InteractableObjectBehavior>();
            return objectBehavior != null && !collider.isTrigger
                && (objectBehavior.Type == InteractableObjectType.Enemy
                || objectBehavior.Type == InteractableObjectType.Player);
        }

        public void CollisionEnter(InteractableObjectBehavior objectBehavior, Collision collision)
        {
            if (collision.relativeVelocity.sqrMagnitude > sqrHitSpeed)
            {
                Damage damage = new Damage()
                {
                    PhysicalDamage = _physicalDamage,
                    StunProbability = _stunProbability
                };

                InteractableObjectBehavior enemy = collision.collider.gameObject.GetComponent<InteractableObjectBehavior>();

                if (enemy != null)
                {
                    enemy.TakeDamageEvent(damage);
                    Debug.Log("Boulder deal damage to " + enemy);
                }
                else
                {
                    Debug.LogError(this + " not found enemy InteractableObjectBehavior");
                }

                Rigidbody rigidbody = objectBehavior.GetComponentInChildren<Rigidbody>();
                rigidbody.isKinematic = true;
                rigidbody.detectCollisions = false;
                //...destroy animation...
                Destroy(objectBehavior.gameObject, _timeToDestroyAfterHit);
            }
        }

        #endregion
    }
}

