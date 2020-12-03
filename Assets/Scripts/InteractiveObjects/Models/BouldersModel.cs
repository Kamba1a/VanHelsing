using Extensions;
using System.Collections.Generic;
using UnityEngine;


namespace BeastHunter
{
    public sealed class BouldersModel : SimpleInteractiveObjectModel
    {
        #region Fields

        private BouldersData _data;
        private InteractableObjectBehavior[] _interactableObjects;

        #endregion


        #region Properties

        public Collider InteractiveTrigger { get; private set; }
        public Rigidbody[] Rigidbodies { get; private set; }
        public Collider[] Colliders { get; private set; }
        public ProjectileBehavior[] Behaviours { get; set; }
        public float Timer { get; set; }
        public InteractableObjectBehavior ActiveObject { get; }

        #endregion


        #region ClassLifeCycle

        public BouldersModel(GameObject prefab, BouldersData data) : base(prefab, data)
        {
            _data = data;

            Rigidbodies = prefab.GetComponentsInChildren<Rigidbody>();
            for (int i = 0; i < Rigidbodies.Length; i++)
            {
                Rigidbodies[i].mass = data.Mass;
                Rigidbodies[i].drag = data.Drag;
                Rigidbodies[i].angularDrag = data.AngularDrag;
                Rigidbodies[i].mass = data.Mass;
            }

            Colliders = prefab.GetComponentsInChildren<SphereCollider>();
            for (int i = 0; i < Colliders.Length; i++)
            { 
                if (!Colliders[i].isTrigger)
                {
                    Colliders[i].material.bounciness = data.Bounciness;
                    break;
                }
            }

            Behaviours = prefab.GetComponentsInChildren<ProjectileBehavior>();
            InteractiveTrigger = prefab.GetComponent<CapsuleCollider>();

            _interactableObjects = prefab.GetComponentsInChildren<InteractableObjectBehavior>();
            ActiveObject = _interactableObjects.GetInteractableObjectByType(InteractableObjectType.ActiveObject);
            InteractiveTrigger = ActiveObject.gameObject.GetComponent<CapsuleCollider>();
        }

        #endregion


        #region SimpleInteractiveObjectModel

        public override void Updating()
        {
            if (IsActivated)
            {
                _data.Act(this);
            }
            else
            {
                base.Updating();
            }
        }

        #endregion

    }
}

