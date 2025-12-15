using UnityEngine;

public class ExplosionItemView : MonoBehaviour
{
    [SerializeField] private ExplosionItem _item;
    
    [SerializeField] private ParticleSystem _explosionEffectPrefab;
    [SerializeField] private GameObject _activateEffect;

    private void Update()
    {
        if (_item.IsActivate)
            ActivationEffect();
        
        if (_item.IsExploded)
            ExplosionEffect();
    }

    private void ExplosionEffect()
    {
        ParticleSystem explosionEffect =
            Instantiate(_explosionEffectPrefab, transform.position, Quaternion.identity, null);
        
        explosionEffect.Play();
    }

    private void ActivationEffect()
    {
        _activateEffect.On();
    }
}
