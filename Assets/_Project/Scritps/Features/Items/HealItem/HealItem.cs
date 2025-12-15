using UnityEngine;

[SelectionBase]
public class HealItem: MonoBehaviour, IHealer
{
    [SerializeField] private float _healthAmount;

    public void HealTarget(IHealable target)
    {
        if(target.CanHeal)
            target.Heal(_healthAmount);
    }
}