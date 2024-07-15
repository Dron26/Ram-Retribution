using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace CompanyName.RamRetribution.Scripts.Boot.Data
{
    [Serializable]
    public class ShopDataState : ISaveable
    {
        //List<ISpell> OpenedSpells;
        //List<ISpell> SelectedSpells;
        public List<SkinTypes> OpenedSkins;
        public SkinTypes SelectedSkin;
        public List<ConfigId> OpenedRams;
        public List<ConfigId> SelectedRams;

        public ShopDataState()
        {
            SelectedSkin = SkinTypes.Default;
            OpenedSkins = new List<SkinTypes>();
            OpenedRams = new List<ConfigId>();
            SelectedRams = new List<ConfigId>();
        }

        public DataNames Name => DataNames.ShopDataState;

        public void OpenSkin(SkinTypes type)
        {
            if (OpenedSkins.Contains(type))
                throw new ArgumentException($"Skin {type} is already open");

            OpenedSkins.Add(type);
        }

        public void OpenSpell()
        {
            
        }

        public void OpenRam(ConfigId configId)
        {
            if(OpenedRams.Contains(configId))
                throw new ArgumentException($"Unit {configId} is already open");

            OpenedRams.Add(configId);
        }
        
        public void SelectRam(ConfigId configId)
        {
            if (OpenedRams.Contains(configId) == false)
                throw new ArgumentException($"Unit {configId} is not opened, but you trying to select him");
            
            SelectedRams.Add(configId);
        }
    }
}