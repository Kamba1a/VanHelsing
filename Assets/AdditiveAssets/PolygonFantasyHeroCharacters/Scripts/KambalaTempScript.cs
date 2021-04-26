using Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KambalaTempScript : MonoBehaviour
{
    [SerializeField] GameObject _modularCharacter;
    [SerializeField] public GameObject _originalArm;
    [SerializeField] public GameObject _copyArm;
    HumanBodyBones[] _bodyBones;
    Transform[] _bonesMap;
    string _moduleName = "Chr_ArmUpperRight_Male_03";


    //private void Start()
    //{
    //    _bonesMap = new Transform[Enum.GetValues(typeof(HumanBodyBones)).Length];

    //    _bodyBones = new HumanBodyBones[Enum.GetValues(typeof(HumanBodyBones)).Length];
    //    int index = 0;
    //    foreach (HumanBodyBones bone in Enum.GetValues(typeof(HumanBodyBones)))
    //    {
    //        _bodyBones[index++] = bone;
    //    }

    //    for (int i = 0; i < _bonesMap.Length; i++)
    //    {
    //        _bonesMap[i] = _copyArm.transform.root.gameObject.GetComponent<Animator>().GetBoneTransform(_bodyBones[i]);
    //    }
    //}

    [ContextMenu("GetBonesListInDebug")]
    public void GetBonesListInDebug()
    {
        Transform[] bones = _originalArm.GetComponent<SkinnedMeshRenderer>().bones;
        foreach (Transform bone in bones)
        {
            Debug.Log(bone.gameObject.name);
        }
    }

    [ContextMenu("CreateNewArm")]
    public void CreateNewArm()
    {
        GameObject arm = _modularCharacter.transform.FindDeep(_moduleName).gameObject;
        GameObject newArm = GameObject.Instantiate(arm, this.gameObject.transform, false);
        SkinnedMeshRenderer skinned = newArm.GetComponent<SkinnedMeshRenderer>();
        skinned.rootBone = this.transform.FindDeep("Clavicle_R");

        Transform[] rootBones = skinned.rootBone.GetComponentsInChildren<Transform>();
        Transform[] newBones = new Transform[rootBones.Length];

        for (int i = 0; i< skinned.bones.Length; i++)
        {
            for (int j = 0; j < rootBones.Length; j++)
            {
                if (skinned.bones[i].name == rootBones[j].name)
                {
                    newBones[i] = rootBones[j];
                }
            }
        }
        rootBones = newBones;
    }

    //[ContextMenu("SetBonesTransformList")]
    //public void SetBonesTransformList()
    //{
    //    _copyArm.GetComponent<SkinnedMeshRenderer>().bones = _bonesMap;

    //    GetBonesListInDebug();
    //}
}
