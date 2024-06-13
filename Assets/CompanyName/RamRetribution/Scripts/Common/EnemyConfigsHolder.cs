using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using UnityEngine;

public class EnemyConfigsHolder : MonoBehaviour
{
    [SerializeField] private UnitConfig _light;
    [SerializeField] private UnitConfig _medium;
    [SerializeField] private UnitConfig _heavy;

    public List<UnitConfig> Get()
    {
        return new List<UnitConfig>()
            {
                _light, 
                _medium,
                _heavy
            };
    }
}
