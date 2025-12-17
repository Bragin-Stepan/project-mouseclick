using System;
using UnityEngine;

public class VFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _clickEffectPrefab;
    [SerializeField] private GameObject _movementIndicatorPrefab;
    
    [SerializeField] private LayerMask _layerMaskToMove;
    
    private AgentCharacter _player;
    private PlayerInput _input;
    private AudionManager _audio;
    
    private GameObject _movementIndicator;
    private ParticleSystem _clickEffect;
    private Vector3 _currentTarget;

    private void Awake()
    {
         _movementIndicator = Instantiate(_movementIndicatorPrefab);
        _movementIndicator.Off();
        
        _clickEffect = Instantiate(_clickEffectPrefab);
        _clickEffect.gameObject.Off();
    }

    private void Update()
    {
        if (_input.OnClick)
            DefaultClickEffect();
    }
    
    private void LateUpdate ()
    {    
        MovementIndicatorEffect();
    }
    
    public void Initialize(AudionManager audio, PlayerInput input, AgentCharacter player)
    {
        _audio = audio;
        _input = input;
        _player = player;
    }

    private void DefaultClickEffect()
    {
        if (RaycastUtils.TryGetHitWithMask(Camera.main, _input.PointPosition, _layerMaskToMove, out RaycastHit hit))
        {
            _clickEffect.gameObject.On();
            _clickEffect.transform.position = hit.point;
            _clickEffect.Play();
            
            _audio.PlaySFX(AudioNameKey.PopLow);
        }
    }

    private void MovementIndicatorEffect()
    {
        if (_player.IsStopped || _player.IsDead)
        {
            _movementIndicator.Off();
            return;
        }
            
        _movementIndicator.transform.position = _player.TargetPosition;
        _movementIndicator.On();
    }
}
