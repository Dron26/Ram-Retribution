using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;

namespace CompanyName.RamRetribution.Scripts.Lobby.GameShop
{
    public class ShopContent
    {
        private List<SkinItem> _skinItems;
        private List<SpellItem> _spellItems;
        private List<RamItem> _ramItems;

        public IReadOnlyList<SkinItem> SkinItems => _skinItems;
        public IReadOnlyList<SpellItem> SpellItems => _spellItems;
        public IReadOnlyList<RamItem> RamItems => _ramItems;

        public void LoadAllAssets()
        {
            _skinItems = Services.ResourceLoadService.LoadAll<SkinItem>(AssetPaths.ShopSkinPrefabs);
            _spellItems = Services.ResourceLoadService.LoadAll<SpellItem>(AssetPaths.ShopSpellPrefabs);
            _ramItems = Services.ResourceLoadService.LoadAll<RamItem>(AssetPaths.ShopRamPrefabs);
            
            ISkill skill = new RageWaveDamage();
            Services.UiDataBinding.SetNewDataForGame(skill);
        }
    }
}