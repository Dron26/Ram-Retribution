using CompanyName.RamRetribution.Scripts.Boot;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.SkillsModule.UI;
using CompanyName.RamRetribution.Scripts.SkillsModule.UI.MVVM;
using CompanyName.RamRetribution.Scripts.SkillsModule.UI.MVVM.Abstraction;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Common.Services
{
    public static class Services
    {
        public static IDataService PrefsDataService { get; private set; }
        public static IResourceLoadService ResourceLoadService { get; private set; }
        public static PauseControl PauseControl { get; private set; }
        public static LvlCombinator LvlCombinator { get; private set; }
        public static GameDataBase GameDataBase { get; private set; }
        public static UiDataBinding UiDataBinding { get; private set; }
        public static ViewModel ViewModel { get; private set; }
        public static Transform LeaderTransform { get; private set; }
        
        private static Model _uiModel;
        
        public static void InitProjectCtx()
        {
            RegisterDataService();
            RegisterResourceLoadService();
            RegisterPauseControl();
            RegisterUiModel();
            RegisterUiViewModel();
            RegisterUiDataBinding();
        }

        public static void InitGameSceneCtx()
        {
            RegisterGameDataBase();
            RegisterLvlCombinator();
        }
        
        public static void RegisterLeader(Transform leaderTransform) 
            => LeaderTransform = leaderTransform;

        private static void RegisterDataService()
            => PrefsDataService = new PrefsDataService(new JsonSerializer());

        private static void RegisterResourceLoadService()
            => ResourceLoadService = new ResourceLoaderService();

        private static void RegisterPauseControl()
            => PauseControl = new PauseControl();
        
        private static void RegisterGameDataBase()
            => GameDataBase = ResourceLoadService
                .Load<GameDataBase>($"{AssetPaths.GameDataBase}{nameof(GameDataBase)}");
        
        private static void RegisterLvlCombinator()
            => LvlCombinator = new LvlCombinator(GameDataBase);
        
        private static void RegisterUiModel()
            => _uiModel = new DefaultUIModel();
        
        private static void RegisterUiDataBinding()
            => UiDataBinding = new UiDataBinding(_uiModel);
        
        private static void RegisterUiViewModel()
            => ViewModel = new DefaultViewModel(_uiModel);
    }
}