using System;
using UnityEngine;

public class DieView : MonoBehaviour
{
     [SerializeField] private Animator _animator;
     [SerializeField] private GameObject _target;
     
     private readonly int _isDeadKey = Animator.StringToHash("IsDead");
     
     private IHealable _healTaker;

     private void Awake()
     {
         if (_target.TryGetComponent(out IHealable healTaker))
             _healTaker = healTaker;
     }

     private void LateUpdate()
     {
         if (_healTaker.IsDead)
             _animator.SetBool(_isDeadKey, true);
     }
}
