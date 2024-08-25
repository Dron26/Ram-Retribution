using CompanyName.RamRetribution.Scripts.Common.Enums;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface ISaveLoadDataService
    {
        public void Save<TSavable>(TSavable data, bool overwrite = true)
            where TSavable : ISavable;

        public TSavable Load<TSavable>(DataNames name)
            where TSavable : ISavable, new();
        
        public void Delete(string name);
    }
}