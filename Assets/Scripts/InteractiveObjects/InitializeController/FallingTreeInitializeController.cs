using UnityEngine;


namespace BeastHunter
{
    public class FallingTreeInitializeController : InteractiveObjectInitializeController
    {
        #region ClassLifeCycle

        public FallingTreeInitializeController(GameContext context) : base(context)
        {
        }

        #endregion


        #region InteractiveObjectInitializeController

        protected override void Initialize()
        {
            GameObject gameobject = Object.Instantiate(Data.FallingTreeData.Prefab, Data.FallingTreeData.PrefabPosition, Quaternion.Euler(Data.FallingTreeData.PrefabEulers));
            _context.InteractableObjectModels.Add(gameobject.GetInstanceID(), new FallingTreeModel(gameobject, Data.FallingTreeData));
        }

        #endregion
    }
}

