namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IEnemyObservable
    {
        public void AddObserver(IEnemyObserver observer);
        public void RemoveObserver(IEnemyObserver observer);
        public void NotifyMoveStarted();
        public void NotifyDamaged(int damage);
    }
}