using UnityEngine;

namespace CompanyName.RamRetribution.Interfaces
{
   public interface IAssets
   {
      public GameObject Instantiate(string path);
      public GameObject Instantiate(string path, Vector3 at);
   }
}