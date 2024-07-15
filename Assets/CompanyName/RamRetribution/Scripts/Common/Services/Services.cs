using System.Collections;
using CompanyName.RamRetribution.Scripts.Boot;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Common.Services
{
    public static class Services
    {
        public static IDataService PrefsDataService { get; private set; }
        public static IResourceLoadService ResourceLoadService { get; private set; }
        public static PauseControl PauseControl { get; private set; }

        //asset ref kit - посмотреть
        //gameDev.ru - сайт с вакансиями
        
        public static void Init()
        {
            RegisterDataService();
            RegisterResourceLoadService();
            RegisterPauseControl();
        }

        private static void RegisterDataService() 
            => PrefsDataService = new PrefsDataService(new JsonSerializer());

        private static void RegisterResourceLoadService() 
            => ResourceLoadService = new ResourceLoaderService();

        private static void RegisterPauseControl() 
            => PauseControl = new PauseControl();
    }
}