using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Ram
{
    public class Movement : IMovable
    {
        public void Move(Vector3 destination)
        {
            Debug.Log("I move");
        }
    }
}