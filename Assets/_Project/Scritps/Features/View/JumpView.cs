using System;
using UnityEngine;

public class JumpView : MonoBehaviour
{
     private readonly int _inJumpProcessKey = Animator.StringToHash("InJumpProcess");

     [SerializeField] private Animator _animator;
     [SerializeField] private GameObject _target;

     private void LateUpdate()
     {
         if (_target.TryGetComponent(out IDirectionalJumpable jumpable))
             _animator.SetBool(_inJumpProcessKey, jumpable.InJumpProcess);
     }
}
