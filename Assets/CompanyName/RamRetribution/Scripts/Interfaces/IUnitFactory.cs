using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Units;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IUnitFactory
    {
        public Unit CreateLeader(LeaderDataState leaderData, Vector3 at);
        public Unit Create(ConfigId configId, Vector3 at);
    }
}