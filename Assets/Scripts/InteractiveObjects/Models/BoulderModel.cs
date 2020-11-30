using UnityEngine;


namespace BeastHunter
{
    public sealed class BoulderModel : SimpleInteractiveObjectModel
    {
        #region Properties

        public Rigidbody Rigidbody { get; private set; }
        public Collider InteractTrigger { get; private set; }

        #endregion


        #region ClassLifeCycle

        public BoulderModel(GameObject gameobject, BoulderData data) : base(gameobject, data)
        {
            Rigidbody = gameobject.GetComponentInChildren<Rigidbody>();
            Rigidbody.mass = data.Mass;
            Rigidbody.drag = data.Drag;
            Rigidbody.angularDrag = data.AngularDrag;
            Rigidbody.transform.gameObject.GetComponent<PhysicMaterial>().bounciness = data.Bounciness; //error

            InteractTrigger = gameobject.GetComponent<SphereCollider>(); //надо который именно триггер
        }

        #endregion
    }
}

