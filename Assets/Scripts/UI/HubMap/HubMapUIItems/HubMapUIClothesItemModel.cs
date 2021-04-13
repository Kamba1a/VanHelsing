﻿using UnityEngine;

namespace BeastHunter
{
    public class HubMapUIClothesItemModel : HubMapUIBaseItemModel
    {
        #region Properties

        public HubMapUIClothesType Type { get; private set; }
        public Material FantasyHeroMaterial { get; private set; }
        public string[] PartsNamesAllGender { get; private set; }
        public string[] PartsNamesMale { get; private set; }
        public string[] PartsNamesFemale { get; private set; }
        public HubMapUICharacterHeadParts[] DisabledHeadParts { get; private set; }

        #endregion


        public HubMapUIClothesItemModel(HubMapUIBaseItemData data) : base(data)
        {
            HubMapUIClothesItemData clothesData = data as HubMapUIClothesItemData;
            Type = clothesData.ClothesType;
            FantasyHeroMaterial = clothesData.FantasyHeroMaterial;
            PartsNamesAllGender = clothesData.ClothesPartsNamesAllGender;
            PartsNamesMale = clothesData.ClothesPartsNamesMale;
            PartsNamesFemale = clothesData.ClothesPartsNamesFemale;
            DisabledHeadParts = clothesData.DisabledHeadParts;
        }
    }
}
