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

            //GameObject gameobject = GameObject.Instantiate(Data.BoulderObjectData.Prefab,
            //    Data.BoulderObjectData.PrefabPosition, Quaternion.Euler(Data.BoulderObjectData.PrefabEulers));
            //_context.InteractableObjectModels.Add(gameobject.GetInstanceID(), new BoulderModel(gameobject, Data.BoulderObjectData));



            #region for BoulderTest Scene:

            GameObject gameobject = GameObject.Instantiate(Data.BoulderObjectData.Prefab,
                new Vector3(-6.70f, 20.52f, 32.1f), Quaternion.Euler(new Vector3(0, -90, 0)));
            _context.InteractableObjectModels.Add(gameobject.GetInstanceID(), new BoulderModel(gameobject, Data.BoulderObjectData));

            GameObject gameobject2 = GameObject.Instantiate(Data.BoulderObjectData.Prefab, 
                new Vector3(-6.70f, 20.52f, 16.08f), Quaternion.Euler(new Vector3(0,-90,0)));
            _context.InteractableObjectModels.Add(gameobject2.GetInstanceID(), new BoulderModel(gameobject2, Data.BoulderObjectData));

            GameObject gameobject3 = GameObject.Instantiate(Data.BoulderObjectData.Prefab,
                new Vector3(-6.70f, 20.52f, 1.33f), Quaternion.Euler(new Vector3(0, -90, 0)));
            _context.InteractableObjectModels.Add(gameobject3.GetInstanceID(), new BoulderModel(gameobject3, Data.BoulderObjectData));

            GameObject gameobject4 = GameObject.Instantiate(Data.BoulderObjectData.Prefab,
                new Vector3(-6.70f, 20.52f, -13.83f), Quaternion.Euler(new Vector3(0, -90, 0)));
            _context.InteractableObjectModels.Add(gameobject4.GetInstanceID(), new BoulderModel(gameobject4, Data.BoulderObjectData));

            GameObject gameobject5 = GameObject.Instantiate(Data.BoulderObjectData.Prefab,
                new Vector3(-6.70f, 20.52f, -29.19f), Quaternion.Euler(new Vector3(0, -90, 0)));
            _context.InteractableObjectModels.Add(gameobject5.GetInstanceID(), new BoulderModel(gameobject5, Data.BoulderObjectData));

            GameObject gameobject6 = GameObject.Instantiate(Data.BoulderObjectData.Prefab,
                new Vector3(6.70f, 20.52f, -29.19f), Quaternion.Euler(new Vector3(0, 90, 0)));
            _context.InteractableObjectModels.Add(gameobject6.GetInstanceID(), new BoulderModel(gameobject6, Data.BoulderObjectData));

            GameObject gameobject7 = GameObject.Instantiate(Data.BoulderObjectData.Prefab,
                new Vector3(6.70f, 20.52f, -13.83f), Quaternion.Euler(new Vector3(0, 90, 0)));
            _context.InteractableObjectModels.Add(gameobject7.GetInstanceID(), new BoulderModel(gameobject7, Data.BoulderObjectData));

            GameObject gameobject8 = GameObject.Instantiate(Data.BoulderObjectData.Prefab,
                new Vector3(6.70f, 20.52f, 1.33f), Quaternion.Euler(new Vector3(0, 90, 0)));
            _context.InteractableObjectModels.Add(gameobject8.GetInstanceID(), new BoulderModel(gameobject8, Data.BoulderObjectData));

            GameObject gameobject9 = GameObject.Instantiate(Data.BoulderObjectData.Prefab,
                new Vector3(6.70f, 20.52f, 16.08f), Quaternion.Euler(new Vector3(0, 90, 0)));
            _context.InteractableObjectModels.Add(gameobject9.GetInstanceID(), new BoulderModel(gameobject9, Data.BoulderObjectData));

            GameObject gameobject10 = GameObject.Instantiate(Data.BoulderObjectData.Prefab,
                new Vector3(6.70f, 20.52f, 32.1f), Quaternion.Euler(new Vector3(0, 90, 0)));
            _context.InteractableObjectModels.Add(gameobject10.GetInstanceID(), new BoulderModel(gameobject10, Data.BoulderObjectData));

            #endregion
        }

        #endregion
    }
}

