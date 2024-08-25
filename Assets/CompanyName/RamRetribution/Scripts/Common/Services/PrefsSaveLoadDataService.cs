using System.IO;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Common.Services
{
    public class PrefsSaveLoadDataService : ISaveLoadDataService
    {
        private readonly ISerializer _serializer;
        private readonly IResourceLoadService _loadService;

        public PrefsSaveLoadDataService(ISerializer serializer, IResourceLoadService loadService)
        {
            _serializer = serializer;
            _loadService = loadService;

#if !UNITY_EDITOR
            Agava.YandexGames.Utility.PlayerPrefs.Load();
#endif
        }

        public void Save<TSaveable>(TSaveable data, bool overwrite = true)
            where TSaveable : ISavable
        {
            if (!overwrite && PlayerPrefs.HasKey(data.Name.ToString()))
                throw new IOException($"File '{data.Name}' is already exists and cannot overwritten");

#if !UNITY_EDITOR && UNITY_WEBGL
            Agava.YandexGames.Utility.PlayerPrefs.SetString(data.Name.ToString(), _serializer.Serialize(data));
            Agava.YandexGames.Utility.PlayerPrefs.Save();
#else
            PlayerPrefs.SetString(data.Name.ToString(), _serializer.Serialize(data));
            PlayerPrefs.Save();
#endif
        }

        public TSaveable Load<TSaveable>(DataNames name)
            where TSaveable : ISavable, new()
        {
            if (!IsExists(name.ToString()))
            {
                var savable = new TSaveable();
                
                if(savable is IInitializableData initializableData)
                    initializableData.Init(_loadService);

                return savable;
            }
            
            string json;

#if !UNITY_EDITOR && UNITY_WEBGL
            json = Agava.YandexGames.Utility.PlayerPrefs.GetString(name);
#else
            json = PlayerPrefs.GetString(name.ToString());
#endif
            return _serializer.Deserialize<TSaveable>(json);
        }

        public void Delete(string name)
        {
            if (IsExists(name))
            {
#if !UNITY_EDITOR && UNITY_WEBGL
                Agava.YandexGames.Utility.PlayerPrefs.DeleteKey(name);
#else
                PlayerPrefs.DeleteKey(name);
#endif
            }
            else
            {
                Debug.LogWarning($"Data with name '{name}' does not exist, " +
                                 $"but you are trying to delete it");
            }
        }

        private bool IsExists(string name)
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            return Agava.YandexGames.Utility.PlayerPrefs.HasKey(name);
#else
            return PlayerPrefs.HasKey(name);
#endif
        }
    }
}