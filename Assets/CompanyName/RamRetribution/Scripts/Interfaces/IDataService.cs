using CompanyName.RamRetribution.Scripts.Boot;
using CompanyName.RamRetribution.Scripts.Common.Enums;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IDataService
    {
        public void Save(ISaveable data, bool overwrite = true);
        public ISaveable Load<T>(string name)
            where T : ISaveable;
        public void Delete(string name);
        public void DeleteAll();
    }
}