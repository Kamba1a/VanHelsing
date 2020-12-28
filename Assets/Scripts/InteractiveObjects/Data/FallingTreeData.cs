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
        [SerializeField] private float _hitSpeed;

        [Header("Rigidbody")]
        [Tooltip("Default: 1000.0")]
        [SerializeField] private float _mass;
        [Tooltip("Default: 0.0")]
        [SerializeField] private float _drag;
        [Tooltip("Default: 0.05")]
        [SerializeField] private float _angularDrag;

        #endregion


        #region Сonstants

        /// <summary>gives the tree time to accelerate before starting the stop check</summary>
        private const float CHECK_VELOCITY_TIME = 2.0f;

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
            _mass = 1000.0f;
            _drag = 0.0f;
            _angularDrag = 0.05f;
            _timeToDeactivate = 20.0f;
            _prefabOffsetY = 0.0f;
            _hitSpeed = 1.0f;
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

        //shows the canvas when entering the trigger
        public override void MakeInteractive(BaseInteractiveObjectModel interactiveObjectModel, 
            ITrigger interactiveTrigger, Collider enteredCollider)
        {
            FallingTreeModel model = interactiveObjectModel as FallingTreeModel;
            if (!model.IsActivated)
            {
                model.CanvasObject.gameObject.SetActive(true);
                model.IsInteractive = true;
            }
        }

        //hides the canvas when exiting the trigger
        public override void MakeNotInteractive(BaseInteractiveObjectModel interactiveObjectModel, 
            ITrigger interactiveTrigger, Collider exitedCollider)
        {
            FallingTreeModel model = interactiveObjectModel as FallingTreeModel;
            if (!model.IsActivated)
            {
                model.IsInteractive = false;
                model.CanvasObject.gameObject.SetActive(false);
            }
        }


        //press key for activate
        public override void Interact(BaseInteractiveObjectModel interactiveObjectModel)
        {
            FallingTreeModel model = interactiveObjectModel as FallingTreeModel;

            if (!model.IsActivated)
            {
                model.IsActivated = true;
                model.IsInteractive = false;
                Activate(model);
            }
        }

        //what happens when activated
        protected override void Activate(SimpleInteractiveObjectModel interactiveObjectModel)
        {
            _activateMsg?.Invoke();

            FallingTreeModel model = interactiveObjectModel as FallingTreeModel;

            model.InteractiveTrigger.enabled = false;
            model.CanvasObject.gameObject.SetActive(false);

            model.DeactivateTimer = _timeToDeactivate;
            model.Rigidbody.isKinematic = false;

            model.DealDamageBehaviour.OnFilterHandler += CollisionFilter;
            model.DealDamageBehaviour.OnTriggerEnterHandler += (p1, p2) => TriggerEnter(p2, model);
            model.DealDamageCollider.enabled = true;
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
            if (_timeToDeactivate - model.DeactivateTimer > CHECK_VELOCITY_TIME)
            {
                if (model.DeactivateTimer <= 0 || model.Rigidbody.velocity == Vector3.zero)
                {
                    model.IsActivated = false;
                    Deactivate(model);
                }
            }

            model.DeactivateTimer -= Time.deltaTime;
        }

        public bool CollisionFilter(Collider collider)
        {
            if (!collider.isTrigger)
            {
                InteractableObjectBehavior objectBehavior = collider.GetComponentInChildren<InteractableObjectBehavior>();
                return objectBehavior != null
                    && (objectBehavior.Type == InteractableObjectType.Enemy
                    || objectBehavior.Type == InteractableObjectType.Player);
            }
            return false;
        }

        public void TriggerEnter(Collider collider, FallingTreeModel model)
        {
            _collisionMsg?.Invoke(collider.gameObject.ToString());

            if (model.Rigidbody.velocity.sqrMagnitude > _sqrHitSpeed)
            {
                InteractableObjectBehavior enemy = collider.GetComponent<InteractableObjectBehavior>();
                DealDamage(enemy, _damage);
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

