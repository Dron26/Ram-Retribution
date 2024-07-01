using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Units;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Common.Services
{
    public interface IUnitFactory
    {
        public Unit CreateLeader(LeaderDataState leaderData, Vector3 at);
        public Unit Create(string configId, Vector3 at);
    }
}