using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Units.Rams;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units.Components
{
    public class RamsPlacementStrategy : IPlacementStrategy
    {
        private readonly int _spaceBetweenMembers = 2;
        private readonly int _stepBetweenLines = 2;
        
        public Vector3 SetPosition(Vector3 origin, Unit unit)
        {
            switch (unit)
            {
                case Leader:
                    return origin;
                case Attacker:
                    return origin + Vector3.left * _spaceBetweenMembers;
                case Demolisher:
                    return origin + Vector3.right * _spaceBetweenMembers;
                case Tank:
                    return origin + Vector3.forward * _stepBetweenLines;
                case Support:
                    return origin + Vector3.back * _stepBetweenLines;
                default: throw new NotImplementedException();
            }
        }
    }
}