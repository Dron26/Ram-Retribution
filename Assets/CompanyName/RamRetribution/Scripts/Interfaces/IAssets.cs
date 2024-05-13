using CompanyName.RamRetribution.Scripts.Ram;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
   public interface IAssets
   {
      public T GetRam<T>(string path)
         where T : Unit;

      public T GetEnemy<T>(string path)
         where T : Enemy;
   }
}