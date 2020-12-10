using System.Collections.Generic;
using UnityEngine;
using Extensions;

namespace BeastHunter
{
    public class HideBushModel : SimpleInteractiveObjectModel
    {
        #region Fields

        private HideBushData _data;
        public HideBushData.BehaviourState State;
        public HashSet<InteractableObjectBehavior> DamageObjects;
        public float BurningTimer;
        public float DealDamageCooldownTimer;
        public GameObject Leaves;
        public GameObject Fire;
        public GameObject Burnt;

        #endregion


        #region ClassLifeCycle

        public HideBushModel(GameObject prefab, HideBushData data) : base(prefab, data)
        {
            _data = data;
            InteractableObjectBehavior[] ioBehaviors = prefab.GetComponentsInChildren<InteractableObjectBehavior>();
            InteractableObjectBehavior bushColliderIO = ioBehaviors.GetInteractableObjectByType(InteractableObjectType.Sphere);
            bushColliderIO.OnFilterHandler += FilterCollision;
            bushColliderIO.OnTriggerEnterHandler += OnTriggerEnter;
            bushColliderIO.OnTriggerExitHandler += OnTriggerExit;
            Collider bushCollider = bushColliderIO.GetComponent<SphereCollider>();

            Leaves = prefab.transform.Find("Leaves").gameObject;
            Fire = prefab.transform.Find("Fire").gameObject;
            Burnt = prefab.transform.Find("Burnt").gameObject;
        }

        #endregion


        #region Methods

        private bool FilterCollision(Collider collider) => _data.FilterCollision(collider, this);
        private void OnTriggerEnter(ITrigger trigger, Collider collider) => _data.TriggerEnter(trigger, collider, this);
        private void OnTriggerExit(ITrigger trigger, Collider collider) => _data.TriggerExit(trigger, collider, this);

        public override void Updating()
        {
            if (State == HideBushData.BehaviourState.Burning)
            {
                _data.Burning(this);
            }
        }

        public void Clean()
        {
            Object.Destroy(Leaves);
            Object.Destroy(Fire);
            Object.Destroy(Prefab.transform.Find("Collider").gameObject);
            Object.Destroy(Prefab.GetComponent<InteractableObjectBehavior>());
            Object.Destroy(Prefab.GetComponent<SphereCollider>());
        }

        #endregion
    }
}

