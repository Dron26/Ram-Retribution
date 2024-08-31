namespace CompanyName.RamRetribution.Scripts.Boot.Data.Interfaces
{
    public interface IInitializableData
    {
        public void Init(IResourceLoadService loadService);
    }
}