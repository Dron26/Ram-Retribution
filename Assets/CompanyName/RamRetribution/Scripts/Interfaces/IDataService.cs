namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IDataService
    {
        public void Save<TSavable>(TSavable data, bool overwrite = true)
            where TSavable : ISavable;
        
        public TSavable Load<TSavable>(string name)
            where TSavable : ISavable, new();
        
        public void Delete(string name);
    }
}