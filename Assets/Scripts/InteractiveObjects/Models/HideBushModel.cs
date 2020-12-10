using System.Collections.Generic;
using UnityEngine;
using Extensions;
using System;

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
        }

        #endregion


        #region Methods

        private bool FilterCollision(Collider collider) => _data.FilterCollision(collider, this);
        private void OnTriggerEnter(ITrigger trigger, Collider collider) => _data.TriggerEnter(trigger, collider, this);
        private void OnTriggerExit(ITrigger trigger, Collider collider) => _data.TriggerExit(trigger, collider, this);
        public override void Updating() => _data.Act(this);

        #endregion
    }
}

