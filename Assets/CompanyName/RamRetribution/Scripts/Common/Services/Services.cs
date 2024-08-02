using CompanyName.RamRetribution.Scripts.Boot;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Skills.Infrastructure;
using CompanyName.RamRetribution.Scripts.Skills.MVVM;
using CompanyName.RamRetribution.Scripts.Skills.UI;

namespace CompanyName.RamRetribution.Scripts.Common.Services
{
    public static class Services
    {
        public static IDataService PrefsDataService { get; private set; }
        public static IResourceLoadService ResourceLoadService { get; private set; }
        public static PauseControl PauseControl { get; private set; }
        public static LvlCombinator LvlCombinator { get; private set; }
        public static GameDataBase GameDataBase { get; private set; }
        public static Model UiModel { get; private set; }
        public static UiDataBinding UiDataBinding { get; private set; }
        public static VIewModel VIewModel { get; private set; }


        public static void Init()
        {
            RegisterDataService();
            RegisterResourceLoadService();
            RegisterPauseControl();
            RegisterLvlCombinator();
        }

        private static void RegisterDataService()
            => PrefsDataService = new PrefsDataService(new JsonSerializer());

        private static void RegisterResourceLoadService()
            => ResourceLoadService = new ResourceLoaderService();

        private static void RegisterPauseControl()
            => PauseControl = new PauseControl();
        private static void RegisterGameDataBase()
            => GameDataBase = new GameDataBase();

        private static void RegisterLvlCombinator()
            => LvlCombinator = new LvlCombinator(GameDataBase);
        private static void RegisterUiModel()
            => UiModel = new DeafaultUIModel();
        private static void RegisterUiDataBinding()
            => UiDataBinding = new UiDataBinding(UiModel);
        private static void RegisterUiViewModel()
            => VIewModel = new DefaultViewModel(UiModel);
    }
}