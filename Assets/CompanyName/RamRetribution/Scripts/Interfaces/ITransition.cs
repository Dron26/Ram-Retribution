namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface ITransition
    {
        public IState ToState { get; }
        public IPredicate Condition { get; }
    }
}