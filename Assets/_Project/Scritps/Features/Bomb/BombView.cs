using UnityEngine;

public class BombView : MonoBehaviour
{
    [SerializeField] private Bomb _bomb;
    
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private GameObject _activateEffect;

    private void Update()
    {
        if (_bomb.IsActivate)
            ActivationEffect();
        
        if (_bomb.IsExplosion)
            ExplosionEffect();
    }

    private void ExplosionEffect()
    {
        _explosionEffect.transform.position = _bomb.transform.position;
        _explosionEffect.Play();
    }

    private void ActivationEffect()
    {
        _activateEffect.On();
    }
}
