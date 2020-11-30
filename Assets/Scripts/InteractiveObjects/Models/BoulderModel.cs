using UnityEngine;


namespace BeastHunter
{
    public sealed class BoulderModel : SimpleInteractiveObjectModel
    {
        #region Fields

        BoulderData _data;

        #endregion


        #region Properties

        public Rigidbody[] Rigidbodies { get; private set; }
        public Collider[] Colliders { get; private set; }
        public Collider InteractiveTrigger { get; private set; }

        #endregion


        #region ClassLifeCycle

        public BoulderModel(GameObject prefab, BoulderData data) : base(prefab, data)
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

            InteractiveTrigger = prefab.GetComponent<CapsuleCollider>();
        }

        public override void Updating()
        {
            base.Updating();

            if (IsActivated)
            {
                _data.Act(this);
            }
        }

            #endregion
        }
}

