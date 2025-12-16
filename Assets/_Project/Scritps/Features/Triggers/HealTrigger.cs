using UnityEngine;

public class HealTrigger: MonoBehaviour
{
    private IHealable _healable;
    
    private void Awake()
    {
        _healable = GetComponentInParent<IHealable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IHealDealer healer))
            healer.HealTarget(_healable);
    }
}