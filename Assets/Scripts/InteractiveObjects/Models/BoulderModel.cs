using UnityEngine;


namespace BeastHunter
{
    public sealed class BoulderModel : SimpleInteractiveObjectModel
    {
        #region Properties

        public Rigidbody Rigidbody { get; private set; }

        #endregion


        #region ClassLifeCycle

        public BoulderModel(GameObject gameobject, BoulderData data) : base(gameobject, data)
        {
            Rigidbody = gameobject.GetComponent<Rigidbody>();
        }

        #endregion
    }
}

