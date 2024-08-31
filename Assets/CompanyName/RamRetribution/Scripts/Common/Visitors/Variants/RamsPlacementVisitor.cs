using CompanyName.RamRetribution.Scripts.Common.Visitors.Interfaces;
using CompanyName.RamRetribution.Scripts.Units.Rams;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Common.Visitors.Variants
{
    public class RamsPlacementVisitor : IRamsVisitor
    {
        private readonly float _spaceBetweenMembers;

        public RamsPlacementVisitor(float spaceBetweenMembers)
            => _spaceBetweenMembers = spaceBetweenMembers;

        public Vector3 Position { get; private set; }

        public void Visit(Ram unit)
            => unit.Accept(this);

        public void Visit(Leader leader)
            => Position = Vector3.zero;

        public void Visit(Tank tank)
            => Position = new Vector3().With(x: 0, y: 0, z: _spaceBetweenMembers);

        public void Visit(Attacker attacker)
            => Position = new Vector3().With(x: _spaceBetweenMembers, y: 0, z: 0);

        public void Visit(Demolisher demolisher)
            => Position = new Vector3().With(x: _spaceBetweenMembers, y: 0, z: 0);

        public void Visit(Support support)
            => Position = new Vector3().With(x: 0, y: 0, z: -_spaceBetweenMembers);
    }
}