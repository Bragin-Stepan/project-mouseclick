using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class HealDealerSpawner: MonoBehaviour
{
    [SerializeField] private GameObject _healDealerPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Vector3 _spawnAreaSize = new Vector3(6, 0, 6);
    [SerializeField] private float _spawnInterval = 5f;
    [SerializeField] private bool _autoSpawn;
    
    private NavMeshHit _navMeshHit;
    private static readonly int MaxAttempts = 10;
    private static readonly float MaxDistanceOffsetRadius = 3f;
    
    private void Start()
    {
        if(_autoSpawn)
            StartAutoSpawn();
    }
    
    private IEnumerator SpawnProcess()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval);

            if (_autoSpawn == false)
                yield break;
            
            TrySpawnHealer();
        }
    }
    
    public IHealDealer Spawn() => SpawnToPoint(_spawnPoint.position);

    public IHealDealer SpawnToPoint(Vector3 position)
    {
        GameObject healerObject = Instantiate(_healDealerPrefab, position, Quaternion.identity, null);

        if (healerObject.TryGetComponent(out IHealDealer healer))
            return healer;
        
        return null;
    }

    public void StartAutoSpawn()
    {
        _autoSpawn = true;
        StartCoroutine(SpawnProcess());
    }

    private void TrySpawnHealer()
    {
        for (int i = 0; i < MaxAttempts; i++)
        {
            Vector3 randomPointHorizontal = _spawnPoint.position + new Vector3(
                Random.Range(-_spawnAreaSize.x, _spawnAreaSize.x / 2),
                0,
                Random.Range(-_spawnAreaSize.z, _spawnAreaSize.z / 2)
            );
            
            if (NavMesh.SamplePosition(randomPointHorizontal, out _navMeshHit, MaxDistanceOffsetRadius, NavMesh.AllAreas))
            {
                Vector3 spawnPoint = _navMeshHit.position + new Vector3(0, _spawnAreaSize.y, 0);
                
                SpawnToPoint(spawnPoint);
                return;
            }
        }
    }
}