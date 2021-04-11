using System;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

namespace PsychoticLab
{
    class CharacterSaver : MonoBehaviour
    {
        [SerializeField] GameObject _torsoPrefab;

        private GameObject _modularCharacters;
        private CharacterRandomizer _characterRandomizer;

        private void Start()
        {
            _modularCharacters = GameObject.Find("ModularCharacters");
            _characterRandomizer = GetComponent<CharacterRandomizer>();
        }

        [ContextMenu("Create new character on scene")]
        public void CreateNewCharacterOnScene()
        {
            GameObject newCharacter = new GameObject("NewCharacter");
            new Preset(_modularCharacters.GetComponent<Animator>()).ApplyTo(newCharacter.AddComponent<Animator>());
            GameObject modularCharacters = new GameObject("Modular_Characters");
            modularCharacters.transform.parent = newCharacter.transform;
            GameObject.Instantiate(_modularCharacters.transform.Find("Root").gameObject, newCharacter.transform).name = "Root";

            for (int i = 0; i < _characterRandomizer.enabledObjects.Count; i++)
            {
                GameObject.Instantiate(_characterRandomizer.enabledObjects[i], modularCharacters.transform, false);
            }
        }

        [ContextMenu("Create character copy on scene")]
        public void CreateCharacterCopyOnScene()
        {
            GameObject newCharacter = GameObject.Instantiate(_modularCharacters, null, false);
            Destroy(newCharacter.GetComponent<CharacterRandomizer>());
            RemoveDisabledChilds(newCharacter);

            GameObject torso = Instantiate(_torsoPrefab, newCharacter.transform, false);
            torso.GetComponent<SkinnedMeshRenderer>().rootBone = newCharacter.transform.Find("Hips");
        }

        private void RemoveDisabledChilds(GameObject gameObject)
        {
            List<GameObject> childs = GetAllChilds(gameObject);
            for (int i = 0; i < childs.Count; i++)
            {
                if (!childs[i].activeSelf)
                {
                    Destroy(childs[i]);
                }
            }
        }

        private List<GameObject> GetAllChilds(GameObject gameObject)
        {
            List<GameObject> childs = new List<GameObject>();
            Transform[] childsTransform = gameObject.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < childsTransform.Length; i++)
            {
                childs.Add(childsTransform[i].gameObject);
            }
            return childs;
        }
    }
}
