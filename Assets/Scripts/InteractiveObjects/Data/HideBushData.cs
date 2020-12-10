using UnityEngine;
using UniRx;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HideBushData", menuName = "CreateData/SimpleInteractiveObjects/HideBushData", order = 0)]
    public sealed class HideBushData : SimpleInteractiveObjectData
    {
        #region PrivateData

        public enum BehaviourState
        {
            None,
            Burning,
            Burned,
        }

        #endregion


        #region Fields

        [SerializeField] private Vector3 _prefabPosition;
        [SerializeField] private Vector3 _prefabEulers;

        [SerializeField] private float _burningTime;
        [SerializeField] private float _dealDamageCooldown;
        [SerializeField] private float _fireDamage;

        #endregion


        #region Properties

        public Vector3 PrefabPosition => _prefabPosition;
        public Vector3 PrefabEulers => _prefabEulers;

        #endregion


        #region ClassLifeCycle

        public HideBushData()
        {
            _burningTime = 20.0f;
            _dealDamageCooldown = 1.5f;
            _fireDamage = 1.0f;
        }

        #endregion


        #region Methods

        public override void MakeInteractive(BaseInteractiveObjectModel interactiveObjectModel,
            ITrigger interactiveTrigger, Collider enteredCollider)
        {
            MessageBroker.Default.Publish(new OnPlayerReachHidePlaceEventClass(true));
        }

        public override void MakeNotInteractive(BaseInteractiveObjectModel interactiveObjectModel,
            ITrigger interactiveTrigger, Collider exitedCollider)
        {
            MessageBroker.Default.Publish(new OnPlayerReachHidePlaceEventClass(false));
            MessageBroker.Default.Publish(new OnPlayerHideEventClass(false));
        }

        protected override void Activate(SimpleInteractiveObjectModel interactiveObjectModel)
        {
            // TODO
        }

        protected override void Deactivate(SimpleInteractiveObjectModel interactiveObjectModel)
        {
            // TODO
        }

        public bool FilterCollision(Collider collider, HideBushModel model)
        {
            InteractableObjectBehavior behaviorIO = collider.GetComponent<InteractableObjectBehavior>();

            switch (model.State)
            {
                case BehaviourState.None:

                    return behaviorIO != null
                    && behaviorIO.Type == InteractableObjectType.Fire;
                    //&& behaviorIO.IsInteractable;     //fire arrows don't work

                case BehaviourState.Burning:

                    return behaviorIO != null
                 && (behaviorIO.Type == InteractableObjectType.Player
                 || behaviorIO.Type == InteractableObjectType.Enemy);

                case BehaviourState.Burned:
                    return false;

                default:
                    return false;
            }
        }

        public void TriggerEnter(ITrigger trigger, Collider collider, HideBushModel model)
        {
            InteractableObjectBehavior behaviorIO = collider.GetComponent<InteractableObjectBehavior>();

            if (behaviorIO.Type == InteractableObjectType.Fire)
            {
                model.State = SetBurningState(model);
            }
            else
            {
                model.DamageObjects.Add(behaviorIO);
            }
        }

        public void TriggerExit(ITrigger trigger, Collider collider, HideBushModel model)
        {
            InteractableObjectBehavior behaviorIO = collider.GetComponent<InteractableObjectBehavior>();
            if (behaviorIO.Type != InteractableObjectType.Fire)
            {
                model.DamageObjects.Remove(behaviorIO);
            }
        }

        public void Act(HideBushModel model)
        {
            switch (model.State)
            {
                case BehaviourState.None:
                    break;

                case BehaviourState.Burning:

                    if (model.BurningTimer <= 0)
                    {
                        model.DamageObjects = null;
                        model.State = BehaviourState.Burned;
                    }

                    if (model.DealDamageCooldownTimer <= 0)
                    {
                        Debug.Log("Fire damage tick");

                        foreach (InteractableObjectBehavior behaviorIO in model.DamageObjects)
                        {
                            behaviorIO.TakeDamageEvent(new Damage() { PhysicalDamage = _fireDamage });    //TODO: FireDamage
                        }

                        model.DealDamageCooldownTimer = _dealDamageCooldown;
                    }

                    model.BurningTimer -= Time.deltaTime;
                    model.DealDamageCooldownTimer -= Time.deltaTime;

                    break;

                case BehaviourState.Burned:
                    break;
            }
        }

        private BehaviourState SetBurningState(HideBushModel model)
        {
            Debug.Log("The bush caught fire");

            model.BurningTimer = _burningTime;
            model.DealDamageCooldownTimer = _dealDamageCooldown;
            model.DamageObjects = new System.Collections.Generic.HashSet<InteractableObjectBehavior>();

            return BehaviourState.Burning;
        }

        #endregion
    }
}

