using System.Linq;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Ram;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CompanyName.RamRetribution.Scripts.Boot
{
    public class SaveLoadService : MonoBehaviour
    {
        private IDataService _fileDataService;
        
        public void Init(IDataService dataService)
        {
            _fileDataService = new FileDataService(new JsonSerializer());
        }
        
        public void Save(GameData data)
            => _fileDataService.Save(data);

        public GameData Load(string saveName)
        {
            GameData gameData = _fileDataService.Load(saveName);
            Bind<TestUnit, LeaderDataState>(gameData.LeaderDataState);
            return gameData;
        }

        private void Bind<T, TData>(TData data)
            where T : MonoBehaviour, IBind<TData>
            where TData : ISave, new()
        {
            var behaviour = FindObjectsByType<T>(FindObjectsSortMode.None).FirstOrDefault();

            if (behaviour is not null)
            {
                if (data == null)
                    data = new TData();
                
                behaviour.Bind(data);
            }
        }
    }
}