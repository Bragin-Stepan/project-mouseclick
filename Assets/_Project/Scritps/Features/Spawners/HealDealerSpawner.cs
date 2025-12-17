using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class HealDealerSpawner: MonoBehaviour
{
    [SerializeField] private GameObject _healDealerPrefab;
    [SerializeField] private Transform _spawnPoint;
    
    [SerializeField] private bool _autoSpawn;
    [SerializeField] private float _autoSpawnInterval = 8f;
    [SerializeField] private Vector3 _autoSpawnAreaSize;
    
    private NavMeshHit _navMeshHit;
    
    private const int MaxAttempts = 10;
    private const float MaxDistanceOffsetRadius = 3f;
    
    private void Start()
    {
        if(_autoSpawn)
            AutoSpawn(true);
    }
    
    private IEnumerator SpawnProcess()
    {
        while (true)
        {
            yield return new WaitForSeconds(_autoSpawnInterval);

            if (_autoSpawn == false)
                yield break;
            
            TrySpawnHealer();
        }
    }
    
    public void AutoSpawn(bool value)
    {
        _autoSpawn = value;
        
        if (value == true)
            StartCoroutine(SpawnProcess());
        else
            StopCoroutine(SpawnProcess());
    }
    
    public IHealDealer Spawn() => SpawnToPoint(_spawnPoint.position);

    public IHealDealer SpawnToPoint(Vector3 position)
    {
        GameObject healerObject = Instantiate(_healDealerPrefab, position, Quaternion.identity, null);

        if (healerObject.TryGetComponent(out IHealDealer healer))
            return healer;
        
        return null;
    }

    private void TrySpawnHealer()
    {
        for (int i = 0; i < MaxAttempts; i++)
        {
            Vector3 randomPointHorizontal = _spawnPoint.position + new Vector3(
                Random.Range(-_autoSpawnAreaSize.x, _autoSpawnAreaSize.x / 2),
                0,
                Random.Range(-_autoSpawnAreaSize.z, _autoSpawnAreaSize.z / 2)
            );
            
            if (NavMesh.SamplePosition(randomPointHorizontal, out _navMeshHit, MaxDistanceOffsetRadius, NavMesh.AllAreas))
            {
                Vector3 spawnPoint = _navMeshHit.position + new Vector3(0, _autoSpawnAreaSize.y, 0);
                
                SpawnToPoint(spawnPoint);
                return;
            }
        }
    }
}