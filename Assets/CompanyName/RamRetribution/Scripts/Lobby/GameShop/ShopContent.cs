using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Lobby.GameShop
{
    public class ShopContent
    {
        private readonly IResourceLoadService _loadService;
        private List<SkinItem> _skinItems;
        private List<SpellItem> _spellItems;
        private List<RamItem> _ramItems;

        public ShopContent(IResourceLoadService loadService) 
            => _loadService = loadService; 
        
        public IReadOnlyList<SkinItem> SkinItems => _skinItems;
        public IReadOnlyList<SpellItem> SpellItems => _spellItems;
        public IReadOnlyList<RamItem> RamItems => _ramItems;

        public void LoadAllAssets()
        {
            _skinItems = _loadService.LoadAll<SkinItem>(AssetPaths.ShopSkinPrefabs);
            _spellItems = _loadService.LoadAll<SpellItem>(AssetPaths.ShopSpellPrefabs);
            _ramItems = _loadService.LoadAll<RamItem>(AssetPaths.ShopRamPrefabs);
        }
    }
}