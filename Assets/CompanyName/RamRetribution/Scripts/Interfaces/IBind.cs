namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IBind<T> 
        where T : ISave
    {
        public void Bind(T data);
    }
}