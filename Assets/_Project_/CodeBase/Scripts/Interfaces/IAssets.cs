using UnityEngine;

namespace _Project_.CodeBase.Scripts.Interfaces
{
   public interface IAssets
   {
      public GameObject Instantiate(string path);
      public GameObject Instantiate(string path, Vector3 at);
   }
}