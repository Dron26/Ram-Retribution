using System.IO;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Common.Services
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
        
        public void Save<TSaveable>(TSaveable data, bool overwrite = true)
        where TSaveable : ISaveable
        {
            string dataPath = GetFilePath(data.Name.ToString());

            if (!overwrite && IsExists(dataPath))
                throw new IOException($"File {data.Name}.{_fileFormat} " +
                                      $"already exists and cannot be overwritten");
            
            File.WriteAllText(dataPath, _serializer.Serialize(data));
        }

        public TSaveable Load<TSaveable>(string name)
        where TSaveable : ISaveable, new()
        {
            string dataPath = GetFilePath(name);

            if (!IsExists(dataPath))
                return new TSaveable();

            return _serializer.Deserialize<TSaveable>(File.ReadAllText(dataPath));
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