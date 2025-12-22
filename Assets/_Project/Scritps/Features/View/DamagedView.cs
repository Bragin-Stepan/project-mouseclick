using System;
using UnityEngine;

public class DamagedView : MonoBehaviour
{
     [SerializeField] private Animator _animator;
     [SerializeField] private GameObject _target;

     private IHealable _healTaker;
     
     private readonly int _damagedKey = Animator.StringToHash("Damaged");

     private void Awake()
     {
         if (_target.TryGetComponent(out IHealable healTaker))
            _healTaker = healTaker;
     }

     private void LateUpdate()
     {
         if (_healTaker.IsDamaged)
         {
             _animator.SetTrigger(_damagedKey);
             _healTaker.SetDamage(false);
         }
     }
}
