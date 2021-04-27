using Extensions;
using UnityEngine;


public class KambalaTempScript : MonoBehaviour
{
    //[SerializeField] private GameObject _originalArm;
    [SerializeField] private GameObject _copiedObject;


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

    [ContextMenu("BonesToDebugLog")]
    public void BonesToDebugLog()
    {
        Transform[] bones = _copiedObject.GetComponent<SkinnedMeshRenderer>().bones;
        foreach (Transform bone in bones)
        {
            Debug.Log($"{bone.gameObject.name} from {bone.transform.root.name}");
        }
    }

    [ContextMenu("UpdateSkinnedMesh")]
    public void UpdateSkinnedMesh()
    {
        //GameObject _copyArm = GameObject.Instantiate(_originalArm, this.gameObject.transform, false);

        SkinnedMeshRenderer copiedSkinnedMesh = _copiedObject.GetComponent<SkinnedMeshRenderer>();
        Transform[] bonesFromOriginal = copiedSkinnedMesh.bones;
        Transform rootBoneFromOriginal = copiedSkinnedMesh.rootBone;

        Transform newRootBone = this.transform.FindDeep(rootBoneFromOriginal.name);
        Transform[] allBonesInNewRootBone = newRootBone.GetComponentsInChildren<Transform>();

        Transform[] newBones = new Transform[bonesFromOriginal.Length];

        for (int i = 0; i < bonesFromOriginal.Length; i++)
        {
            for (int j = 0; j < allBonesInNewRootBone.Length; j++)
            {
                if (bonesFromOriginal[i].name == allBonesInNewRootBone[j].name)
                {
                    newBones[i] = allBonesInNewRootBone[j];
                }
            }
        }

        copiedSkinnedMesh.bones = newBones;
        copiedSkinnedMesh.rootBone = newRootBone;
    }
}
