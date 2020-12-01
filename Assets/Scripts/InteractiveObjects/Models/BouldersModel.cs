using UnityEngine;


namespace BeastHunter
{
    public sealed class BouldersModel : SimpleInteractiveObjectModel
    {
        #region Fields

        BouldersData _data;

        #endregion


        #region Properties

        public Collider InteractiveTrigger { get; private set; }
        public Rigidbody[] Rigidbodies { get; private set; }
        public Collider[] Colliders { get; private set; }
        public ProjectileBehavior[] Behaviours { get; set; }

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

