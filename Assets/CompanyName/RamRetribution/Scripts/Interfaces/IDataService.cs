using CompanyName.RamRetribution.Scripts.Boot;
using CompanyName.RamRetribution.Scripts.Common.Enums;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IDataService
    {
        public void Save<T>(T data, bool overwrite = true)
            where T : ISaveable;
        public T Load<T>(string name)
            where T : ISaveable;
        public void Delete(string name);
        public void DeleteAll();
    }
}