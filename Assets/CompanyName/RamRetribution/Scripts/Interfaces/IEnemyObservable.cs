namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface  IEnemyObservable
    {
        void AddObserver(IEnemyObserver observer);
        void RemoveObserver(IEnemyObserver observer);
        void NotifyMoveStarted();
        void NotifyDamaged(int damage);
    }
}