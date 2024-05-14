using CompanyName.RamRetribution.Scripts.Common.AssetLoad;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Ram;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private Transform _ramContainer;
        
        private void Awake()
        {

        }

        private T LoadAsset<T>() 
            where T : Unit
        {
            IAssets assetProvider = new AssetProvider();
            var prefab = assetProvider.Get<T>($"{AssetPath.RamDataPath}{typeof(T).Name}");
            return prefab;
        }
    }
}