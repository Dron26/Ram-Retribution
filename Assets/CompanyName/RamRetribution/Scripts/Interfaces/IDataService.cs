using CompanyName.RamRetribution.Scripts.Boot;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IDataService
    {
        public void Save(GameData data, bool overwrite = true);
        public GameData Load(string name);
        public void Delete(string name);
        public void DeleteAll();
    }
}