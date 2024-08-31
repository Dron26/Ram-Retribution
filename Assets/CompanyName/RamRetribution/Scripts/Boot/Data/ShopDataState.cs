using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Boot.Data.Interfaces;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;

namespace CompanyName.RamRetribution.Scripts.Boot.Data
{
    [Serializable]
    public class ShopDataState : ISavable
    {
        public List<ConfigId> OpenedRams = new();
        public List<ConfigId> SelectedRams = new();
        public List<SpellsId> OpenedSpells = new();
        public List<SpellsId> SelectedSpells = new();
        public List<SkinsId> OpenedSkins = new();
        public SkinsId SelectedSkin = SkinsId.Default;

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

            if (SelectedRams.Count >= GameConstants.MaxRamsWithoutLeader)
                SelectedRams.RemoveAt(SelectedRams.Count - 1);
            
            SelectedRams.Add(configId);
        }

        #endregion
    }
}