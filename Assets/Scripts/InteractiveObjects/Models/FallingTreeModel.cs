using Extensions;
using System.Collections.Generic;
using UnityEngine;


namespace BeastHunter
{
    public sealed class FallingTreeModel : SimpleInteractiveObjectModel
    {
        #region Fields

        private FallingTreeData _data;
        private InteractableObjectBehavior[] _interactableObjects;

        #endregion


        #region Properties

        public Collider InteractiveTrigger { get; }
        public Rigidbody Rigidbody { get; private set; }
        public InteractableObjectBehavior FallingTreeBehaviour { get; }
        public float Timer { get; set; }
        /// <summary>contains boolean values of boulders that contains information about whether the boulder has been added to the destroy queue</summary>
        public Dictionary<int, bool> DestroyInfoDictionary { get; private set; }

        #endregion


        #region ClassLifeCycle

        public FallingTreeModel(GameObject prefab, FallingTreeData data) : base(prefab, data)
        {
            _data = data;
            DestroyInfoDictionary = new Dictionary<int, bool>();

            Services.SharedInstance.PhysicsService.FindGround(Prefab.transform.position, out Vector3 groundPosition);
            Prefab.transform.position = new Vector3(groundPosition.x, groundPosition.y+_data.PrefabOffsetY, groundPosition.z);

            Rigidbody = prefab.GetComponentInChildren<Rigidbody>();
            Rigidbody.mass = data.Mass;
            Rigidbody.drag = data.Drag;
            Rigidbody.angularDrag = data.AngularDrag;
            Rigidbody.mass = data.Mass;

            _interactableObjects = prefab.GetComponentsInChildren<InteractableObjectBehavior>();
            InteractiveTrigger = _interactableObjects.GetInteractableObjectByType(InteractableObjectType.ActiveObject).GetComponent<CapsuleCollider>();

            FallingTreeBehaviour = _interactableObjects.GetInteractableObjectByType(InteractableObjectType.HitBox);
            PhysicMaterial physicMaterial = new PhysicMaterial() { bounciness = data.Bounciness, dynamicFriction = 0, staticFriction = 0 };
            FallingTreeBehaviour.GetComponent<SphereCollider>().material = physicMaterial;
            DestroyInfoDictionary.Add(FallingTreeBehaviour.GetInstanceID(), false);
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
            DestroyInfoDictionary = null;

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

            if (Rigidbody != null)
            {
                Object.Destroy(Rigidbody);
            }
        }

        #endregion
    }
}

