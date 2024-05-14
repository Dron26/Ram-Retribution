using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Ram;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Common.Visitors
{
    public class OpenedRamsVisitor : IRamVisitor
    {
        private readonly PlayerData _data;

        public OpenedRamsVisitor(PlayerData data)
        {
            _data = data;
        }
        
        public bool IsOpened { get; private set; }

        public void Visit(Unit unit)
        {
            unit.Accept(this);
        }

        public void Visit(Support support)
        {

        }

        public void Visit(Tank tank)
        {
            
        }

        public void Visit(Leader leader)
        {

        }

        public void Visit(Demolisher demolisher)
        {

        }

        public void Visit(Attacker attacker)
        {

        }
    }
}