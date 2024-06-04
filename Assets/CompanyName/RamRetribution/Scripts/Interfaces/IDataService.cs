using CompanyName.RamRetribution.Scripts.Boot;
using CompanyName.RamRetribution.Scripts.Common.Enums;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IDataService
    {
        public void Save<TSaveable>(TSaveable data, bool overwrite = true)
            where TSaveable : ISaveable;
        public TSaveable Load<TSaveable>(string name)
            where TSaveable : ISaveable, new();
        public void Delete(string name);
        public void DeleteAll();
    }
}