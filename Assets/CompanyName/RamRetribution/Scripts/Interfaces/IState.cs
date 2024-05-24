namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IState
    {
        public void Enter();
        public void Exit();
        public void Update();
    }
}