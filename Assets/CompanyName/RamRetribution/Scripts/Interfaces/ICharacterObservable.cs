namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface  ICharacterObservable
    {
        void AddObserver(ICharacterObserver observer);

        void RemoveObserver(ICharacterObserver observer);

        void NotifyMoveStart();

        void NotifyDamageTaken();

    }
}