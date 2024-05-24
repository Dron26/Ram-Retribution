using System;
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
        }
        
        public void Save<T>(T data, bool overwrite = true)
        where T : ISaveable
        {
            if (!overwrite && PlayerPrefs.HasKey(data.Name.ToString()))
                throw new IOException($"File '{data.Name}' is already exists and cannot overwritten");
            
            PlayerPrefs.SetString(data.Name.ToString(), _serializer.Serialize(data));
            PlayerPrefs.Save();
        }

        public T Load<T>(string name)
        where T : ISaveable
        {
            if (!IsExists(name))
                throw new ArgumentException($"No data with name '{name}'");
            
            string json = PlayerPrefs.GetString(name);
            
            return _serializer.Deserialize<T>(json);
        }

        public void Delete(string name)
        {
            if (IsExists(name))
                PlayerPrefs.DeleteKey(name);
            else
                Debug.LogWarning($"Data with name '{name}' does not exist, " +
                                 $"but you are trying to delete it");
        }

        public void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }
        
        private bool IsExists(string name) 
            => PlayerPrefs.HasKey(name);
    }
}