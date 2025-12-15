using System;
using UnityEngine;
using UnityEngine.AI;

public class Game : MonoBehaviour
{
    [SerializeField] private AgentCharacter _player;
    [SerializeField] private LayerMask _layerMaskToMove;
    [SerializeField] private VFX _vfx;
    [SerializeField] private CameraController _camera;

    private Controller _characterController;
    private PlayerInput _input;

    private NavMeshQueryFilter _navMeshFilter;

    private void Awake()
    {
        _input = new ();
        
        _navMeshFilter = new ()
        {
            agentTypeID = 0,
            areaMask = 1
        };

        _vfx.Initialize(_input, _player);
        _camera.Initialize(_input);
        
        _characterController = new PlayerAgentMoveToPointController(
            _player,
            _player,
            _input,
            _layerMaskToMove,
            _navMeshFilter);
        
        _characterController.Enable();
    }

    private void Update()
    {
        _characterController.Update(Time.deltaTime);

        if (_player.IsDead)
        {
            _player.Stop();
            _characterController.Disable();
        }
    }
}
