using System;
using UnityEngine;

public class HealthBarView : MonoBehaviour
{
     [SerializeField] private GameObject _target;
     [SerializeField] private SliderUI _healthBar;

     private void LateUpdate()
     {
         if (_target.TryGetComponent(out IHealable healTaker))
         {
             _healthBar.RotateTo(Camera.main.transform.forward);
             _healthBar.SetValue(healTaker.HealthPercent);
         }
     }
}
