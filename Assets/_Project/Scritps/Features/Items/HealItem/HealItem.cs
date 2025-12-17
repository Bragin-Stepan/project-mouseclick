using UnityEngine;

[SelectionBase]
public class HealItem: MonoBehaviour, IHealDealer
{
    [SerializeField] private float _healthAmount;

    public void HealTarget(IHealable target)
    {
        if(target.CanHeal)
        {
            target.Heal(_healthAmount);
            Destroy(gameObject);
        }
    }
    
    public void HealTargets(IHealable[] targets)
    {
        foreach (IHealable target in targets)
        {
            if(target.CanHeal)
            {
                target.Heal(_healthAmount);
                Destroy(gameObject);
            }
        }
    }
}