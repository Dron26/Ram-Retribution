namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface ISerializer
    {
        public string Serialize<T>(T obj, bool goodPrint = false);
        public T Deserialize<T>(string json);
    }
}