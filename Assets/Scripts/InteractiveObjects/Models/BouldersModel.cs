using Extensions;
using System.Collections.Generic;
using UnityEngine;


namespace BeastHunter
{
    public sealed class BouldersModel : SimpleInteractiveObjectModel
    {
        #region Fields

        private BouldersData _data;
        private Collider[] _colliders;
        private InteractableObjectBehavior[] _interactableObjects;
        private InteractableObjectBehavior _activeObject;

        #endregion


        #region Properties

        public Collider InteractiveTrigger { get; private set; }
        public Rigidbody[] Rigidbodies { get; private set; }
        public List<InteractableObjectBehavior> BoulderBehaviours { get; private set; }
        public float Timer { get; set; }

        #endregion


        #region ClassLifeCycle

        public BouldersModel(GameObject prefab, BouldersData data) : base(prefab, data)
        {
            _data = data;

            Services.SharedInstance.PhysicsService.FindGround(Prefab.transform.position, out Vector3 groundPosition);
            Prefab.transform.position = new Vector3(groundPosition.x, groundPosition.y+_data.OffsetY, groundPosition.z);

            Rigidbodies = prefab.GetComponentsInChildren<Rigidbody>();
            for (int i = 0; i < Rigidbodies.Length; i++)
            {
                Rigidbodies[i].mass = data.Mass;
                Rigidbodies[i].drag = data.Drag;
                Rigidbodies[i].angularDrag = data.AngularDrag;
                Rigidbodies[i].mass = data.Mass;
            }

            _colliders = prefab.GetComponentsInChildren<SphereCollider>();
            for (int i = 0; i < _colliders.Length; i++)
            { 
                if (!_colliders[i].isTrigger)
                {
                    _colliders[i].material.bounciness = data.Bounciness;
                    break;
                }
            }

            _interactableObjects = prefab.GetComponentsInChildren<InteractableObjectBehavior>();
            _activeObject = _interactableObjects.GetInteractableObjectByType(InteractableObjectType.ActiveObject);
            InteractiveTrigger = _activeObject.GetComponent<CapsuleCollider>();
            BoulderBehaviours = _interactableObjects.GetInteractableObjectsByType(InteractableObjectType.HitBox);
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


        #region Methods

        public void Clean()
        {
            _data = null;

            if (CanvasObject != null)
            {
                Object.Destroy(CanvasObject);
            }

            if (InteractiveTrigger != null)
            {
                Object.Destroy(InteractiveTrigger);
            }

            for (int i = 0; i< _interactableObjects.Length; i++)
            {
                if (_interactableObjects[i] != null)
                {
                    Object.Destroy(_interactableObjects[i]);
                }
            }
            _interactableObjects = null;

            for (int i = 0; i < Rigidbodies.Length; i++)
            {
                if (Rigidbodies[i] != null)
                {
                    Object.Destroy(Rigidbodies[i]);
                }
            }
            Rigidbodies = null;
        }

        #endregion
    }
}

