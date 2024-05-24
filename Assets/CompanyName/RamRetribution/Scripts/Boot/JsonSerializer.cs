using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Boot
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize<T>(T obj, bool goodPrint = false) 
            => JsonUtility.ToJson(obj, goodPrint);

        public T Deserialize<T>(string json) 
            => JsonUtility.FromJson<T>(json);
    }
}