using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private float _cameraMoveSpeed = 20f;
    
    private PlayerInput _input;

    private const float DeadZone = 0.1f;

    public void Initialize(PlayerInput input)
    {
        _input = input;
    }

    private void LateUpdate()
    {
        if (_input.Direction.magnitude > DeadZone)
            _cameraTarget.position += _input.Direction * (_cameraMoveSpeed * Time.deltaTime);
    }
}
