using UnityEngine;

public interface IAssets
{
    GameObject Instantiate(string path);
    GameObject Instantiate(string path, Vector3 at);
}