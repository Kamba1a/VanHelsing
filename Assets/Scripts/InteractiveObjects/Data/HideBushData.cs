using UnityEngine;
using UniRx;
using System.Collections.Generic;
using Extensions;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HideBushData", menuName = "CreateData/SimpleInteractiveObjects/HideBushData", order = 0)]
    public sealed class HideBushData : SimpleInteractiveObjectData
    {
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

        public bool FilterCollision(Collider collider, bool isBurning)
        {
            InteractableObjectBehavior behaviorIO = collider.GetComponent<InteractableObjectBehavior>();

            if (!isBurning)
            {
                return behaviorIO != null
                    && behaviorIO.Type == InteractableObjectType.Fire;
                    //&& behaviorIO.IsInteractable;     //fire arrows don't work
            }
            else
            {
                return behaviorIO != null
                    && (behaviorIO.Type == InteractableObjectType.Player
                    || behaviorIO.Type == InteractableObjectType.Enemy);
            }
        }

        public void TriggerEnter(Collider collider, HideBushModel model)
        {
            InteractableObjectBehavior behaviorIO = collider.GetComponent<InteractableObjectBehavior>();

            if (behaviorIO.Type == InteractableObjectType.Fire)
            {
                SetBurningState(model);
            }
            else
            {
                model.DamageObjects.Add(behaviorIO);
            }
        }

        public void TriggerExit(Collider collider, HashSet<InteractableObjectBehavior> damageObjects)
        {
            InteractableObjectBehavior behaviorIO = collider.GetComponent<InteractableObjectBehavior>();
            if (behaviorIO.Type != InteractableObjectType.Fire)
            {
                damageObjects.Remove(behaviorIO);
            }
        }

        public void Burning(HideBushModel model)
        {
            if (model.BurningTimer <= 0)
            {
                Debug.Log("The bush burned");

                model.IsBurning = false;
                model.DamageObjects.Clear();
                model.Burnt.SetActive(true);
                model.Clean();
            }

            if (model.DealDamageCooldownTimer <= 0)
            {
                Debug.Log("Fire damage tick");

                HashSet<int> damageDone = new HashSet<int>();
                foreach (InteractableObjectBehavior behaviorIO in model.DamageObjects)
                {
                    int gameObjectID = behaviorIO.transform.GetMainParent().GetInstanceID();
                    if (!damageDone.Contains(gameObjectID)) //to avoid causing double damage to the object in the case of multiple colliders with behaviorIO
                    {
                        behaviorIO.TakeDamageEvent(new Damage() { PhysicalDamage = _fireDamage });    //TODO: FireDamage
                        damageDone.Add(gameObjectID);
                    }
                }
                model.DealDamageCooldownTimer = _dealDamageCooldown;
            }

            model.BurningTimer -= Time.deltaTime;
            model.DealDamageCooldownTimer -= Time.deltaTime;
        }

        private void SetBurningState(HideBushModel model)
        {
            Debug.Log("The bush caught fire");

            model.DamageObjects = new HashSet<InteractableObjectBehavior>();
            model.Fire.SetActive(true);
            model.BurningTimer = _burningTime;
            model.DealDamageCooldownTimer = _dealDamageCooldown;
            model.IsBurning = true;
        }
            #endregion
    }
}

