using UnityEngine;

public class TransformRotatorDirection: IDirectionalRotator
{
    private const float DeadZone = 0.05f;

    private  float _speed;
    private readonly Transform _transform;
    
    private Vector3 _currentDirection;

    public TransformRotatorDirection(Transform transform)
    {
        _transform = transform;
    }

    public Quaternion CurrentRotation => _transform.rotation;

    public void Update(float deltaTime)
    {
        if (_currentDirection.sqrMagnitude < DeadZone)
            return;

        Quaternion lookRotation = Quaternion.LookRotation(_currentDirection.normalized);
        float step = _speed * deltaTime;

        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, step);
    }

    public void Rotate(Vector3 direction, float speed)
    {
        _speed = speed;
        _currentDirection = direction;
    }
}
