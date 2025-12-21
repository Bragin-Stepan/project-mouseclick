using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    [SerializeField] private AgentCharacter _playerPrefab;
    [SerializeField] private Transform _startPoint;
    
    [SerializeField] private AudionManager _audio;
    [SerializeField] private VFX _vfx;
    [SerializeField] private CameraController _camera;
    
    [SerializeField] private LayerMask _layerMaskToMove;
    [SerializeField] private HealDealerSpawner _spawner;
    
    private Controller _characterController;
    private PlayerInput _input;
    private AgentCharacter _player;

    private NavMeshQueryFilter _navMeshFilter;

    private bool _autoSpawn;

    private void Awake()
    {
        _player = Instantiate(_playerPrefab, _startPoint.position,  Quaternion.identity, null);
        _input = new ();
        
        _navMeshFilter = new ()
        {
            agentTypeID = 0,
            areaMask = 1
        };

        _vfx.Initialize(_audio, _input, _player);
        _camera.Initialize(_input);
        
        _characterController = new PlayerAgentMoveToPointController(
            _player,
            _player,
            _player,
            _input,
            _layerMaskToMove,
            _navMeshFilter);
        
        _characterController.Enable();
    }

    private void Start()
    {
        _audio.StartBGM(AudioNameKey.Gameplay);
    }

    private void Update()
    {
        _characterController.Update(Time.deltaTime);

        if (_input.OnInteract)
            SpawnHandler();
                
        if (_player.IsDead)
        {
            _player.Stop();
            _characterController.Disable();
        }
    }

    private void SpawnHandler()
    { 
        _autoSpawn = !_autoSpawn;
        _spawner.AutoSpawn(_autoSpawn);
    }
}
