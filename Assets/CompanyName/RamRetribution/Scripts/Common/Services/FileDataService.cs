using System;
using System.IO;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Boot
{
    public class FileDataService : IDataService
    {
        private readonly ISerializer _serializer;
        private readonly string _dataPath;
        private readonly string _fileFormat;

        public FileDataService(ISerializer serializer)
        {
            _dataPath = Application.persistentDataPath;
            _fileFormat = "json";
            _serializer = serializer;
        }
        
        public void Save<T>(T data, bool overwrite = true)
        where T : ISaveable
        {
            string dataPath = GetFilePath(data.Name.ToString());

            if (!overwrite && IsExists(dataPath))
                throw new IOException($"File {data.Name}.{_fileFormat} " +
                                      $"already exists and cannot be overwritten");
            
            File.WriteAllText(dataPath, _serializer.Serialize(data));
        }

        public T Load<T>(string name)
        where T : ISaveable
        {
            string dataPath = GetFilePath(name);

            if (!IsExists(dataPath))
                throw new ArgumentException($"No data with name {name}");

            return _serializer.Deserialize<T>(File.ReadAllText(dataPath));
        }

        public void Delete(string name)
        {
            string dataPath = GetFilePath(name);
            
            if(IsExists(dataPath))
                File.Delete(dataPath);
        }

        public void DeleteAll()
        {
            foreach (string filePath in Directory.GetFiles(_dataPath))
                File.Delete(filePath);
        }

        private bool IsExists(string path)
            => File.Exists(path);

        private string GetFilePath(string fileName)
            => Path.Combine(_dataPath, string.Concat(fileName, ".", _fileFormat));
    }
}