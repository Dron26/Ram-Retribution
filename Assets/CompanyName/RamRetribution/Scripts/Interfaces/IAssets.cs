using CompanyName.RamRetribution.Scripts.Ram;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
   public interface IAssets
   {
      public T Get<T>(string path)
         where T : Object;
   }
}