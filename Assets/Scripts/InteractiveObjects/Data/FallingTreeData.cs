using Extensions;
using System;
using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "FallingTreeData", menuName = "CreateData/SimpleInteractiveObjects/FallingTreeData", order = 0)]
    public sealed class FallingTreeData : SimpleInteractiveObjectData, IDealDamage
    {
        #region SerializeFields

        [Header("FallingTreeData")]
        [SerializeField] private bool _debugMessages;
        [SerializeField] private Damage _damage;
        [SerializeField] private Vector3 _prefabPosition;
        [SerializeField] private Vector3 _prefabEulers;
        [SerializeField] private float _prefabOffsetY;
        [Tooltip ("The force with which fallingTree move from place. Default: 100.0")]
        [SerializeField] private float _pushingForce;
        [Tooltip("The time after which the physics is turned off for all fallingTree. Default: 30.0")]
        [SerializeField] private float _timeToDeactivate;
        [Tooltip("Time after which the boulder disappears after collision and deal damage. Default: 1.0")]
        [SerializeField] private float _timeToDestroyAfterHit;
        [Tooltip("The speed at which the boulder deals damage. Default: 2.0")]
        [SerializeField] private float _hitSpeed;

        [Header("Rigidbody")]
        [Tooltip("Default: 40.0")]
        [SerializeField] private float _mass;
        [Tooltip("Default: 0.1")]
        [SerializeField] private float _drag;
        [Tooltip("Default: 0.75")]
        [SerializeField] private float _angularDrag;
        [Tooltip("Default: 0.1")]
        [SerializeField] private float _bounciness;

        #endregion


        #region Сonstants

        /// <summary>gives the boulder time to accelerate before starting the stop check</summary>
        private const float CHECK_VELOCITY_TIME = 2.0f;
        /// <summary>protects the player from taking damage when fallingTree activated</summary>
        private const float CHECK_COLLISION_TIME = 1.0f;

        #endregion


        #region Fields

        private float _sqrHitSpeed;
        private Action _activateMsg;
        private Action _deactivateMsg;
        private Action _destroyRigidbodyMsg;
        private Action<string> _dealDamageMsg;
        private Action<string> _collisionMsg;
        private Action _destroyAfterHitMsg;

        #endregion


        #region Properties

        public Vector3 PrefabPosition => _prefabPosition ;
        public Vector3 PrefabEulers => _prefabEulers;
        public float PushingForce => _pushingForce;
        public float Mass => _mass;
        public float Drag => _drag;
        public float AngularDrag => _angularDrag;
        public float Bounciness => _bounciness;
        public float PrefabOffsetY => _prefabOffsetY;

        #endregion


        #region ClassLifeCycle

        public FallingTreeData()
        {
            _pushingForce = 100.0f;
            _mass = 40.0f;
            _drag = 0.1f;
            _angularDrag = 0.75f;
            _bounciness = 0.1f;
            _timeToDeactivate = 30.0f;
            _timeToDestroyAfterHit = 1.0f;
            _hitSpeed = 2;
            _prefabOffsetY = 0.3f;
        }

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            _sqrHitSpeed = _hitSpeed * _hitSpeed;
            DebugMessages(_debugMessages);
        }

        #endregion

        #region SimpleInteractiveObjectData

        //shows the canvas when entering the boulder trigger
        public override void MakeInteractive(BaseInteractiveObjectModel interactiveObjectModel, 
            ITrigger interactiveTrigger, Collider enteredCollider)
        {
            (interactiveObjectModel as FallingTreeModel).CanvasObject.gameObject.SetActive(true);
            interactiveObjectModel.IsInteractive = true;
        }

        //hides the canvas when exiting the boulder trigger
        public override void MakeNotInteractive(BaseInteractiveObjectModel interactiveObjectModel, 
            ITrigger interactiveTrigger, Collider exitedCollider)
        {
            interactiveObjectModel.IsInteractive = false;
            (interactiveObjectModel as FallingTreeModel).CanvasObject.gameObject.SetActive(false);
        }


        //press key for activate
        public override void Interact(BaseInteractiveObjectModel interactiveObjectModel)
        {
            FallingTreeModel model = interactiveObjectModel as FallingTreeModel;

            if (!model.IsActivated)
            {
                model.IsActivated = true;
                Activate(model);
            }
        }

        //what happens when activated
        protected override void Activate(SimpleInteractiveObjectModel interactiveObjectModel)
        {
            _activateMsg?.Invoke();

            FallingTreeModel model = interactiveObjectModel as FallingTreeModel;
            model.IsInteractive = false;
            Destroy(model.InteractiveTrigger);
            Destroy(model.CanvasObject.gameObject);
            model.Timer = _timeToDeactivate;

            model.Rigidbody.isKinematic = false;
            model.Rigidbody.AddForce(model.Prefab.transform.forward * PushingForce, ForceMode.Impulse);

            model.FallingTreeBehaviour.OnFilterHandler += CollisionFilter;
            model.FallingTreeBehaviour.OnCollisionEnterHandler += (p1,p2) => CollisionEnter(p1, p2, model);
        }

        //what happens when deactivated
        protected override void Deactivate(SimpleInteractiveObjectModel interactiveObjectModel)
        {
            _deactivateMsg?.Invoke();

            FallingTreeModel model = interactiveObjectModel as FallingTreeModel;
            model.Clean();
        }

        #endregion


        #region FallingTreeModel

        //update
        public void Act(FallingTreeModel model)
        {
            if (_timeToDeactivate - model.Timer > CHECK_VELOCITY_TIME)
            {
                if (model.Rigidbody != null && model.Rigidbody.velocity == Vector3.zero)
                {
                    Destroy(model.Rigidbody);
                    _destroyRigidbodyMsg?.Invoke();
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

        public void CollisionEnter(InteractableObjectBehavior objectBehavior, Collision collision, FallingTreeModel model)
        {
            int objectBehaviorID = objectBehavior.GetInstanceID();
            if (!model.DestroyInfoDictionary[objectBehaviorID])
            {
                _collisionMsg?.Invoke(collision.collider.gameObject.ToString());

                if (collision.relativeVelocity.sqrMagnitude > _sqrHitSpeed && _timeToDeactivate - model.Timer > CHECK_COLLISION_TIME)
                {
                    model.DestroyInfoDictionary[objectBehaviorID] = true;
                    InteractableObjectBehavior enemy = collision.collider.GetComponent<InteractableObjectBehavior>();

                    if (enemy != null)
                    {
                        DealDamage(enemy, _damage);
                    }
                    else
                    {
                        Debug.LogError(this + " not found InteractableObjectBehavior at " + collision.gameObject);
                    }

                    Rigidbody rigidbody = objectBehavior.GetComponentInChildren<Rigidbody>();
                    rigidbody.isKinematic = true;
                    rigidbody.detectCollisions = false;
                    //...destroy animation...
                    Destroy(objectBehavior.gameObject, _timeToDestroyAfterHit);
                    _destroyAfterHitMsg?.Invoke();
                }
            }
        }

        #endregion


        #region Methods

        /// <summary>Subscribes to debug message delegates</summary>
        /// <param name="switcher">on/off debug messages</param>
        private void DebugMessages(bool switcher)
        {
            if (switcher)
            {
                _activateMsg = () => Debug.Log("FallingTree activated");
                _deactivateMsg = () => Debug.Log("FallingTree deactivated");
                _destroyRigidbodyMsg = () => Debug.Log("Destroy fallingTree rigidbody (velocity = 0,0,0)");
                _dealDamageMsg = (enemy) => Debug.Log("FallingTree deal damage to " + enemy);
                _collisionMsg = (gameobject) => Debug.Log("FallingTree collision with " + gameobject);
                _destroyAfterHitMsg = () => Debug.Log("FallingTree will destroy after " + _timeToDestroyAfterHit + " sec");
            }
        }

        #endregion


        #region IDealDamage

        public void DealDamage(InteractableObjectBehavior enemy, Damage damage)
        {
            Damage countDamage = Services.SharedInstance.AttackService
                .CountDamage(damage, enemy.transform.GetMainParent().gameObject.GetInstanceID());

            _dealDamageMsg?.Invoke(enemy.ToString());
            enemy.TakeDamageEvent(countDamage);

        }

        #endregion
    }
}

