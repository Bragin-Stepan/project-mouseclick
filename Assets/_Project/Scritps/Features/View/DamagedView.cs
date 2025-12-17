using System;
using UnityEngine;

public class DamagedView : MonoBehaviour
{
     private readonly int _damagedKey = Animator.StringToHash("Damaged");

     [SerializeField] private Animator _animator;
     [SerializeField] private GameObject _target;

     private void LateUpdate()
     {
         if (_target.TryGetComponent(out IHealable healTaker) && healTaker.IsDamaged)
         {
             _animator.SetTrigger(_damagedKey);
             healTaker.SetDamage(false);
         }
     }
}
