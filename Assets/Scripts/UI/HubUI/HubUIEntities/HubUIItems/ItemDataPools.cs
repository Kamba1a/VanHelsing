using System;
using System.Collections.Generic;
using UnityEngine;


namespace BeastHunterHubUI
{
    [CreateAssetMenu(fileName = "ItemDataPools", menuName = "CreateData/HubUIData/ItemDataPools", order = 0)]
    public class ItemDataPools : ScriptableObject
    {
        #region Fields

        [SerializeField] private ClothesItemData[] _clothesItemsPool;
        [SerializeField] private WeaponItemData[] _weaponItemsPool;
        [SerializeField] private PocketItemData[] _pocketItemsPool;

        private Dictionary<int, List<WeaponItemData>> _weaponDataPoolDic;
        private Dictionary<int, Dictionary<ClothesType, List<ClothesItemData>>> _clothesDataPoolDic;

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            InitializeWeaponDataPoolDictionary();
            InitializeClothesDataPoolDictionary();
        }

        private void OnDisable()
        {
            _weaponDataPoolDic.Clear();
            _clothesDataPoolDic.Clear();

            _weaponDataPoolDic = null;
            _weaponDataPoolDic = null;
        }

        #endregion


        #region Methods

        public PocketItemData GetRandomPocketItem()
        {
            return _pocketItemsPool[UnityEngine.Random.Range(0, _pocketItemsPool.Length)];
        }

        public List<ClothesItemData> GetClothesDataListByRankAndType(int rank, ClothesType clothesType)
        {
            if (_clothesDataPoolDic.ContainsKey(rank))
            {
                return _clothesDataPoolDic[rank][clothesType];
            }
            else
            {
                Debug.LogError("No clothes of the requested rank");
                return null;
            }
        }

        public ClothesItemData GetRandomClothesDataByRankAndType(int rank, ClothesType clothesType)
        {
            List<ClothesItemData> clothesList = GetClothesDataListByRankAndType(rank, clothesType);
            return clothesList[UnityEngine.Random.Range(0, clothesList.Count)];
        }

        public List<WeaponItemData> GetWeapondataListByRank(int rank)
        {
            if (_weaponDataPoolDic.ContainsKey(rank))
            {
                return _weaponDataPoolDic[rank];
            }
            else
            {
                Debug.LogError("No weapon of the requested rank");
                return null;
            }
        }

        public WeaponItemData GetRandomWeaponDataByRank(int rank)
        {
            List<WeaponItemData> weaponDataList = GetWeapondataListByRank(rank);
            return weaponDataList[UnityEngine.Random.Range(0, weaponDataList.Count)];
        }

        public WeaponItemData GetRandomWeaponDataByRanks(int[] ranks)
        {
            List<int> ranksChecked = new List<int>();

            for (int i = 0; i < ranks.Length; i++)
            {
                if (_weaponDataPoolDic.ContainsKey(ranks[i]))
                {
                    ranksChecked.Add(ranks[i]);
                }
            }

            int randomRankIndex = UnityEngine.Random.Range(0, ranksChecked.Count);
            return GetRandomWeaponDataByRank(randomRankIndex);
        }

        public ClothesItemData GetRandomClothesDataByRanksAndType(int[] ranks, ClothesType clothesType)
        {
            List<int> ranksChecked = new List<int>();

            for (int i = 0; i < ranks.Length; i++)
            {
                if (_clothesDataPoolDic.ContainsKey(ranks[i]))
                {
                    ranksChecked.Add(ranks[i]);
                }
            }

            int randomRankIndex = UnityEngine.Random.Range(0, ranksChecked.Count);
            return GetRandomClothesDataByRankAndType(randomRankIndex, clothesType);
        }

        private void InitializeWeaponDataPoolDictionary()
        {
            _weaponDataPoolDic = new Dictionary<int, List<WeaponItemData>>();

            if(_weaponItemsPool != null)
            {
                for (int i = 0; i < _weaponItemsPool.Length; i++)
                {
                    int itemRank = _weaponItemsPool[i].Rank;
                    if (!_weaponDataPoolDic.ContainsKey(itemRank))
                    {
                        _weaponDataPoolDic.Add(itemRank, new List<WeaponItemData>());
                    }
                    _weaponDataPoolDic[itemRank].Add(_weaponItemsPool[i]);
                }
            }
        }

        private void InitializeClothesDataPoolDictionary()
        {
            _clothesDataPoolDic = new Dictionary<int, Dictionary<ClothesType, List<ClothesItemData>>>();

            if(_clothesItemsPool != null)
            {
                for (int i = 0; i < _clothesItemsPool.Length; i++)
                {
                    int itemRank = _clothesItemsPool[i].Rank;
                    if (!_clothesDataPoolDic.ContainsKey(itemRank))
                    {
                        Dictionary<ClothesType, List<ClothesItemData>> clothesTypesDic = new Dictionary<ClothesType, List<ClothesItemData>>();
                        foreach (ClothesType clothesType in Enum.GetValues(typeof(ClothesType)))
                        {
                            clothesTypesDic.Add(clothesType, new List<ClothesItemData>());
                        }
                        _clothesDataPoolDic.Add(itemRank, clothesTypesDic);

                    }
                    _clothesDataPoolDic[itemRank][_clothesItemsPool[i].ClothesType].Add(_clothesItemsPool[i]);
                }
            }
        }

        #endregion
    }
}
