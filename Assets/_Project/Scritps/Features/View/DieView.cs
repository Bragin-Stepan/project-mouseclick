using System;
using UnityEngine;

public class DieView : MonoBehaviour
{
     private readonly int _isDeadKey = Animator.StringToHash("IsDead");

     [SerializeField] private Animator _animator;
     [SerializeField] private GameObject _target;

     private void LateUpdate()
     {
         if (_target.TryGetComponent(out IHealable healTaker) && healTaker.IsDead)
             _animator.SetBool(_isDeadKey, true);
     }
}
