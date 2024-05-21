namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IBind<T> 
        where T : ISaveable
    {
        public void Bind(T data);
    }
}