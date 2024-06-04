using System.IO;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Common.Services
{
    public class PrefsDataService : IDataService
    {
        private readonly ISerializer _serializer;

        public PrefsDataService(ISerializer serializer)
        {
            _serializer = serializer;

#if !UNITY_EDITOR
            Agava.YandexGames.Utility.PlayerPrefs.Load();
#endif
        }

        public void Save<TSaveable>(TSaveable data, bool overwrite = true)
            where TSaveable : ISaveable
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

        public TSaveable Load<TSaveable>(string name)
            where TSaveable : ISaveable, new()
        {
            if (!IsExists(name))
                return new TSaveable();

            string json;

#if !UNITY_EDITOR && UNITY_WEBGL
            json = Agava.YandexGames.Utility.PlayerPrefs.GetString(name);
#else
            json = PlayerPrefs.GetString(name);
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

        public void DeleteAll()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            Agava.YandexGames.Utility.PlayerPrefs.DeleteAll();
#else
            PlayerPrefs.DeleteAll();
#endif
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