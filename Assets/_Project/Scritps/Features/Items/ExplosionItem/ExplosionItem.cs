using System.Collections;
using UnityEngine;

[SelectionBase]
public class ExplosionItem : MonoBehaviour
{
    [SerializeField] private float _explosionDamage;
    [SerializeField] private float _timeToExplosion;
    [SerializeField] private float _detectRadius;
    [SerializeField] private float _explosionRadius;
    
    public bool IsActivate { get ; private set; }
    public bool IsExploded { get ; private set; }

    private void FixedUpdate()
    {
        if (TryDetectTarget())
            StartCoroutine(DetonationProcess());
    }

    private bool TryDetectTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _detectRadius);
        
        foreach (Collider collider in colliders)
            if (collider.TryGetComponent( out IDamageable damageable))
            {
                IsActivate = true;
                return true;
            }

        return false;
    }
    
    private IEnumerator DetonationProcess()
    {
        yield return new WaitForSeconds(_timeToExplosion - 0.01f);
        IsExploded = true;
        
        yield return new WaitForSeconds(0.01f); // костыль
        Explode();
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        
        foreach (Collider collider in colliders)
            if (collider.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(_explosionDamage);
        
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _detectRadius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}

// Проще наверное было через коллайдеры, но в задании есть пункт отрисовки через Gizmos
// Так что решил сделать через рейкасты