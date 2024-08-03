using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Boot.Data
{
    [Serializable]
    public class ShopDataState : ISaveable
    {
        public List<ConfigId> OpenedRams;
        public List<ConfigId> SelectedRams;
        public List<SpellsId> OpenedSpells;
        public List<SpellsId> SelectedSpells;
        public List<SkinsId> OpenedSkins;
        public SkinsId SelectedSkin;
        
        public ShopDataState()
        {
            OpenedRams = new List<ConfigId>();
            SelectedRams = new List<ConfigId>();
            OpenedSpells = new List<SpellsId>();
            SelectedSpells = new List<SpellsId>();
            OpenedSkins = new List<SkinsId>();
            SelectedSkin = SkinsId.Default;
        }
        
        public DataNames Name => DataNames.ShopDataState;

        #region Skins

        public void OpenSkin(SkinsId type)
        {
            if (OpenedSkins.Contains(type))
                throw new ArgumentException($"Skin {type} is already open");

            OpenedSkins.Add(type);
        }

        #endregion

        #region Spells

        public void OpenSpell(SpellsId spellsId)
        {
            if(OpenedSpells.Contains(spellsId))
                throw new ArgumentException($"Spell {spellsId} is already open");
            
            OpenedSpells.Add(spellsId);
        }

        public void SelectSpell(SpellsId spellsId)
        {
            if(OpenedSpells.Contains(spellsId) == false)
                throw new ArgumentException($"Spell {spellsId} is not opened, but you trying to select him");
            
            if (SelectedSpells.Count >= GameConstants.MaxSpells)
                SelectedSpells.RemoveAt(SelectedSpells.Count - 1);
            
            SelectedSpells.Add(spellsId);
        }

        #endregion

        #region Rams

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

            if (SelectedRams.Count >= GameConstants.MaxRams)
                SelectedRams.RemoveAt(SelectedRams.Count - 1);
            
            SelectedRams.Add(configId);
        }

        #endregion
    }
}