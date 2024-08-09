namespace CompanyName.RamRetribution.Scripts.Units.Components.Buffs.Interfaces
{
    public interface IBuff<T>
        where T : IImprovable 
    {
        public void Apply(T entity);
    }
}