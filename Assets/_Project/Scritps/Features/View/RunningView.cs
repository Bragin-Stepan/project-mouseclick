using System;
using UnityEngine;

public class RunningView : MonoBehaviour
{
     [SerializeField] private Animator _animator;
     [SerializeField] private GameObject _target;

     private const float MovementDeadZone = 0.05f;
     
     private IDirectionalMovable _movable;
     
     private readonly int _isRunningKey = Animator.StringToHash("IsRunning");

     private void Awake()
     {
         if (_target.TryGetComponent(out IDirectionalMovable movable))
             _movable = movable;
     }

     private void LateUpdate()
     {
          RunningProcess(_movable.CurrentVelocity.magnitude > MovementDeadZone);
     }
     
     private void RunningProcess(bool value) => _animator.SetBool(_isRunningKey, value);
}
