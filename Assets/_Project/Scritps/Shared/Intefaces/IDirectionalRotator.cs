using UnityEngine;

public interface IDirectionalRotator 
{
    Quaternion CurrentRotation  { get; }
    void Rotate(Vector3 direction, float speed);
    void Update(float deltaTime);
}
