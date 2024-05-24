using CompanyName.RamRetribution.Scripts.Ram;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
   public interface IResourceLoadService
   {
      public T Load<T>(string path)
         where T : Object;
   }
}