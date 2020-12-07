﻿using System;
using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "BouldersData", menuName = "CreateData/SimpleInteractiveObjects/BouldersData", order = 0)]
    public sealed class BouldersData : SimpleInteractiveObjectData
    {
        #region SerializeFields

        [Header("BouldersData")]
        [SerializeField] private bool _debugMessages;
        [SerializeField] private Vector3 _prefabPosition;
        [SerializeField] private Vector3 _prefabEulers;
        [SerializeField] private float _prefabOffsetY;
        [Tooltip ("The force with which boulders move from place. Default: 100.0")]
        [SerializeField] private float _pushingForce;
        [Tooltip("The time after which the physics is turned off for all boulders. Default: 30.0")]
        [SerializeField] private float _timeToDeactivate;
        [Tooltip("Time after which the boulder disappears after collision and deal damage. Default: 1.0")]
        [SerializeField] private float _timeToDestroyAfterHit;
        [Tooltip("The speed at which the boulder deals damage. Default: 2.0")]
        [SerializeField] private float _hitSpeed;

        [Header("Damage")]
        [SerializeField] private float _physicalDamage;
        [SerializeField] private float _stunProbability;

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

        #endregion


        #region Fields

        private float _sqrHitSpeed;
        private Action _activateMsg;
        private Action _deactivateMsg;
        private Action _destroyRigidbodyMsg;
        private Action<InteractableObjectBehavior> _dealDamageMsg;

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

        public BouldersData()
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


        //press key for activate
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
            _activateMsg?.Invoke();

            BouldersModel model = interactiveObjectModel as BouldersModel;
            model.IsInteractive = false;
            Destroy(model.InteractiveTrigger);
            Destroy(model.CanvasObject.gameObject);
            model.Timer = _timeToDeactivate;

            for (int i = 0; i < model.Rigidbodies.Length; i++)
            {
                model.Rigidbodies[i].isKinematic = false;
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
            _deactivateMsg?.Invoke();

            BouldersModel model = interactiveObjectModel as BouldersModel;
            model.Clean();
        }

        #endregion


        #region BouldersModel

        //update
        public void Act(BouldersModel model)
        {
            if (_timeToDeactivate - model.Timer > CHECK_VELOCITY_TIME)
            {
                for (int i = 0; i < model.Rigidbodies.Length; i++)
                {
                    if (model.Rigidbodies[i] != null && model.Rigidbodies[i].velocity == Vector3.zero)
                    {
                        Destroy(model.Rigidbodies[i]);
                        _destroyRigidbodyMsg?.Invoke();
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
            if (collision.relativeVelocity.sqrMagnitude > _sqrHitSpeed)
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
                    _dealDamageMsg?.Invoke(enemy);
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


        #region Methods

        /// <summary>Subscribes to debug message delegates</summary>
        /// <param name="switcher">on/off debug messages</param>
        private void DebugMessages(bool switcher)
        {
            if (switcher)
            {
                _activateMsg = () => Debug.Log("Boulders activated");
                _deactivateMsg = () => Debug.Log("Boulders deactivated");
                _destroyRigidbodyMsg = () => Debug.Log("Destroy boulder rigidbody (velocity = 0,0,0)");
                _dealDamageMsg = (enemy) => Debug.Log("Boulder deal damage to " + enemy);
            }
        }

        #endregion
    }
}

