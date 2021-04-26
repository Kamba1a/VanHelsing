using System;
using System.Collections.Generic;
using UnityEngine;


namespace BeastHunterHubUI
{
    public class ItemsPoolsModel
    {
        #region Fields

        private Dictionary<int, List<WeaponItemData>> _weaponPool;
        private Dictionary<int, Dictionary<ClothesType, List<ClothesItemData>>> _clothesPool;

        #endregion


        #region ClassLifeCycle

        public ItemsPoolsModel(ItemsPoolsData data)
        {
            for (int i = 0; i < data.WeaponItemsPool.Length; i++)
            {
                int itemRank = data.WeaponItemsPool[i].Rank;
                if (!_weaponPool.ContainsKey(itemRank))
                {
                    _weaponPool.Add(itemRank, new List<WeaponItemData>());
                }
                _weaponPool[itemRank].Add(data.WeaponItemsPool[i]);
            }

            for (int i = 0; i < data.ClothesItemsPool.Length; i++)
            {
                int itemRank = data.ClothesItemsPool[i].Rank;
                if (!_clothesPool.ContainsKey(itemRank))
                {
                    Dictionary<ClothesType, List<ClothesItemData>> clothesTypesDic = new Dictionary<ClothesType, List<ClothesItemData>>();
                    foreach (ClothesType clothesType in Enum.GetValues(typeof(ClothesType)))
                    {
                        clothesTypesDic.Add(clothesType, new List<ClothesItemData>());
                    }
                    _clothesPool.Add(itemRank, clothesTypesDic);

                }
                _clothesPool[itemRank][data.ClothesItemsPool[i].ClothesType].Add(data.ClothesItemsPool[i]);
            }
        }

        #endregion


        #region Methods

        public List<ClothesItemData> GetClothesDataListByRankAndType(int rank, ClothesType clothesType)
        {
            if (_clothesPool.ContainsKey(rank))
            {
                return _clothesPool[rank][clothesType];
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
            if (_weaponPool.ContainsKey(rank))
            {
                return _weaponPool[rank];
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
                if (_weaponPool.ContainsKey(ranks[i]))
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
                if (_clothesPool.ContainsKey(ranks[i]))
                {
                    ranksChecked.Add(ranks[i]);
                }
            }

            int randomRankIndex = UnityEngine.Random.Range(0, ranksChecked.Count);
            return GetRandomClothesDataByRankAndType(randomRankIndex, clothesType);
        }

        #endregion
    }
}
