using UnityEngine;

public interface IFactory
{
    GameObject Create(Vector3 at);
}