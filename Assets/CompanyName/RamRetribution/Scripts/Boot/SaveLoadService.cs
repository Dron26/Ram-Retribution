using System.Linq;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Ram;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Boot
{
    public class SaveLoadService : MonoBehaviour
    {
        private IDataService _fileDataService;

        public void Init(IDataService dataService)
        {
            _fileDataService = dataService;
        }
        
        public void Save<T>(T data, bool overwrite = true)
            where T : ISaveable
            => _fileDataService.Save(data, overwrite);

        public void Load<T>(DataNames dataName)
            where T : ISaveable
        {
            ISaveable savedData = _fileDataService.Load<T>(dataName.ToString());
            
            switch (dataName)
            {
                case DataNames.LeaderDataState:
                    LeaderDataState leaderData = savedData as LeaderDataState;
                    Bind<TestUnit, LeaderDataState>(leaderData);
                    break;
                case DataNames.LevelData:
                    LevelData levelData = savedData as LevelData;
                    break;
                case DataNames.GateData:
                    GateData gateData = savedData as GateData;
                    Bind<Gate.Gate, GateData>(gateData);
                    break;
                case DataNames.HiredRamsData:
                    HiredRamsData ramsData = savedData as HiredRamsData;
                    break;
            }
        }

        private void Bind<T, TData>(TData data)
            where T : MonoBehaviour, IBind<TData>
            where TData : ISaveable, new()
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