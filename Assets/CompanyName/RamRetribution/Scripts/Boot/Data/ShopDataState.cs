using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Boot.Data
{
    [Serializable]
    public class ShopDataState : ISaveable
    {
        //List<ISpell> OpenedSpells;
        //List<ISpell> SelectedSpells;
        public List<SkinTypes> OpenedSkins;
        public SkinTypes SelectedSkin;

        public ShopDataState()
        {
            SelectedSkin = SkinTypes.Default;
            OpenedSkins = new List<SkinTypes>();
        }

        public DataNames Name { get; } = DataNames.ShopDataState;

        public void OpenSkin(SkinTypes type)
        {
            if (OpenedSkins.Contains(type))
                throw new ArgumentException($"Skin {type} is already open");

            OpenedSkins.Add(type);
        }

        public void OpenSpell()
        {
            
        }
    }
}