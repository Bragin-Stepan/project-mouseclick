using System;
using UnityEngine;

public class RunningView : MonoBehaviour
{
     private readonly int _isRunningKey = Animator.StringToHash("IsRunning");

     [SerializeField] private Animator _animator;
     [SerializeField] private GameObject _target;

     private const float MovementDeadZone = 0.05f;

     private void LateUpdate()
     {
         if (_target.TryGetComponent(out IDirectionalMovable movable))
            RunningProcess(movable.CurrentVelocity.magnitude > MovementDeadZone);
     }
     
     private void RunningProcess(bool value) => _animator.SetBool(_isRunningKey, value);
}
