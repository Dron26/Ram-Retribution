using CompanyName.RamRetribution.Scripts.Boot;
using CompanyName.RamRetribution.Scripts.Common.AssetLoad;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Common.Services
{
    public static class Services
    {
        public static IDataService PrefsDataService { get; private set; }
        public static IResourceLoadService ResourceLoadService { get; private set; }

        public static void Init()
        {
            RegisterDataService();
            RegisterResourceLoadService();
        }

        private static void RegisterDataService()
        {
            PrefsDataService = new PrefsDataService(new JsonSerializer());
        }

        private static void RegisterResourceLoadService()
        {
            ResourceLoadService = new ResourceLoaderService();
        }
    }
}