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
        [Tooltip("The time after which the tree physics is turned off. Default: 20.0")]
        [SerializeField] private float _timeToDeactivate;

        [Header("Rigidbody")]
        [Tooltip("Default: 100.0")]
        [SerializeField] private float _mass;
        [Tooltip("Default: 0.0")]
        [SerializeField] private float _drag;
        [Tooltip("Default: 0.05")]
        [SerializeField] private float _angularDrag;

        #endregion


        #region Сonstants

        /// <summary>gives the tree time to accelerate before starting the stop check</summary>
        private const float CHECK_VELOCITY_TIME = 2.0f;
        /// <summary>protects the player from taking damage when falling tree activated</summary>
        private const float CHECK_COLLISION_TIME = 1.0f;
        private const float CHECK_RELATIVE_VELOCITY = 0.1f;

        #endregion


        #region Fields

        private float _sqrHitSpeed;
        private Action _activateMsg;
        private Action _deactivateMsg;
        private Action<string> _dealDamageMsg;
        private Action<string> _collisionMsg;

        #endregion


        #region Properties

        public Vector3 PrefabPosition => _prefabPosition ;
        public Vector3 PrefabEulers => _prefabEulers;
        public float Mass => _mass;
        public float Drag => _drag;
        public float AngularDrag => _angularDrag;
        public float PrefabOffsetY => _prefabOffsetY;

        #endregion


        #region ClassLifeCycle

        public FallingTreeData()
        {
            _mass = 100.0f;
            _drag = 0.0f;
            _angularDrag = 0.05f;
            _timeToDeactivate = 20.0f;
            _prefabOffsetY = 0.0f;
        }

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            _sqrHitSpeed = CHECK_RELATIVE_VELOCITY * CHECK_RELATIVE_VELOCITY;
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
                if (model.Timer <= 0 || model.Rigidbody.velocity == Vector3.zero)
                {
                    model.IsActivated = false;
                    Deactivate(model);
                }
            }
            model.Timer -= Time.deltaTime;
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
            _collisionMsg?.Invoke(collision.collider.gameObject.ToString());

            if (collision.relativeVelocity.sqrMagnitude > _sqrHitSpeed && _timeToDeactivate - model.Timer > CHECK_COLLISION_TIME)
            {
                InteractableObjectBehavior enemy = collision.collider.GetComponent<InteractableObjectBehavior>();

                if (enemy != null)
                {
                    DealDamage(enemy, _damage);
                }
                else
                {
                    Debug.LogError(this + " not found InteractableObjectBehavior at " + collision.gameObject);
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
                _dealDamageMsg = (enemy) => Debug.Log("FallingTree deal damage to " + enemy);
                _collisionMsg = (gameobject) => Debug.Log("FallingTree collision with " + gameobject);
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

