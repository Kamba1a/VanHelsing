using UnityEngine;


namespace BeastHunter
{
    public class BoulderInitializeController : InteractiveObjectInitializeController
    {
        #region ClassLifeCycle

        public BoulderInitializeController(GameContext context) : base(context)
        {
        }

        #endregion


        #region IAwake

        protected override void Initialize()
        {
            var boulderData = Data.BoulderObjectData;

            if (boulderData.Prefab == null)
            {
                boulderData.Prefab = Resources.Load<GameObject>("Boulder");
            }

            GameObject gameobject = GameObject.Instantiate(Data.BoulderObjectData.Prefab, Data.BoulderObjectData.PrefabPosition, Quaternion.identity);
            _context.InteractableObjectModels.Add(gameobject.GetInstanceID(), new BoulderModel(gameobject, Data.BoulderObjectData));
        }

        #endregion
    }
}

