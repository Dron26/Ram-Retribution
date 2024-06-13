using CompanyName.RamRetribution.Scripts.Units;
using CompanyName.RamRetribution.Scripts.Units.Components;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    [SerializeField] private AIMovement _aiMovement;

    private Camera _camera;
    
    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit))
            {
                _aiMovement.Move(hit.point);
            }
        }
    }
}
