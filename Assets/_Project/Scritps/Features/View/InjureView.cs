using System;
using UnityEngine;

public class InjureView : MonoBehaviour
{
     [SerializeField] private Animator _animator;
     [SerializeField] private GameObject _target;

     private int _injureLayerIndex;
     
     private const float InjureHealthPercent = 0.3f;

     private const float MinLayerWeightValue = 0;
     private const float MaxLayerWeightValue = 1;
     
     private const string InjureLayerName = "InjuredLayer";

     private void Awake()
     {
         _injureLayerIndex = _animator.GetLayerIndex(InjureLayerName);
     }

     private void LateUpdate()
     {
         if (_target.TryGetComponent(out IHealable healTaker))
         {
             if (healTaker.HealthPercent < InjureHealthPercent)
                 _animator.SetLayerWeight(_injureLayerIndex, MaxLayerWeightValue);
             else
                 _animator.SetLayerWeight(_injureLayerIndex, MinLayerWeightValue);
         }
     }
}
