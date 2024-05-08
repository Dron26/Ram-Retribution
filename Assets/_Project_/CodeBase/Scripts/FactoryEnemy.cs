using UnityEngine;

public class FactoryEnemy : IFactory
{
    private readonly IAssets _assets;

    public FactoryEnemy(IAssets assets)
    {
        _assets = assets;
    }

    public GameObject Create(Vector3 at)
    {
        GameObject spawnerPrefab = _assets.Instantiate(AssetPath.Spawner, at);
        return spawnerPrefab;
    }
}