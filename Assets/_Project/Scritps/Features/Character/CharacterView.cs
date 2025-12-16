using System;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
     private const float MovementDeadZone = 0.05f;
     private const float InjureHealthPercent = 0.3f;

     private const float MinLayerWeightValue = 0;
     private const float MaxLayerWeightValue = 1;
     
     private const string InjureLayerName = "InjuredLayer";
     private const string GetHitLayerName = "GetHitLayer";
     
     private readonly int _isRunningKey = Animator.StringToHash("IsRunning");
     private readonly int _damagedKey = Animator.StringToHash("Damaged");
     private readonly int _isDeadKey = Animator.StringToHash("IsDead");
     private readonly int _inJumpProcessKey = Animator.StringToHash("InJumpProcess");

     [SerializeField] private Animator _animator;
     [SerializeField] private AgentCharacter _character;
     [SerializeField] private SliderUI _healthBar;

     private int _injureLayerIndex;
     private int _getHitLayerIndex;

     private void Awake()
     {
         _injureLayerIndex = _animator.GetLayerIndex(InjureLayerName);
         _getHitLayerIndex = _animator.GetLayerIndex(GetHitLayerName);
     }

     private void LateUpdate()
     {
         if (_character == null)
             return;
         
         if (_character.IsDamaged)
             Damaged();
         
         if (_character.IsDead)
            Die();
         
         RunningProcess(_character.CurrentVelocity.magnitude > MovementDeadZone);
         InjureProcess();
         HealthBarProcess();
         GetHitProcess();
         InJumpProcess();
     }

     private void InJumpProcess() => _animator.SetBool(_inJumpProcessKey, _character.InJumpProcess);

     private void HealthBarProcess()
     {
         _healthBar.RotateTo(Camera.main.transform.forward);
         _healthBar.SetValue(_character.HeathPercent);
     }

     private void InjureProcess()
     {
         if (_character.HeathPercent < InjureHealthPercent)
             _animator.SetLayerWeight(_injureLayerIndex, MaxLayerWeightValue);
         else
             _animator.SetLayerWeight(_injureLayerIndex, MinLayerWeightValue);
     }
     
     private void GetHitProcess()
     {
         if (_character.IsDamaged)
             _animator.SetLayerWeight(_getHitLayerIndex, MaxLayerWeightValue);
         else
             _animator.SetLayerWeight(_getHitLayerIndex, MinLayerWeightValue);
     }

     private void RunningProcess(bool value) => _animator.SetBool(_isRunningKey, value);

     private void Damaged()
     {
         _animator.SetTrigger(_damagedKey);
         _character.SetDamage(false);
     }

     private void Die() => _animator.SetBool(_isDeadKey, true);

}
