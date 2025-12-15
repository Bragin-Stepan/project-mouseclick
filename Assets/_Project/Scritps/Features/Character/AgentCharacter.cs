using UnityEngine;
using UnityEngine.AI;

[SelectionBase]
public class AgentCharacter : MonoBehaviour, IDirectionalRotatable, IDirectionalMovable, IDamageable, IHealable
{
    private AgentMover _mover;
    private IDirectionalRotator _rotator;
    private Health _health;
    private NavMeshAgent _agent;
    
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _rotateSpeed = 800;
    [SerializeField] private float _maxHealth = 60;
    
    public bool IsStopped => _mover.IsStopped;
    public Vector3 Position => transform.position;
    public Vector3 TargetPosition => _agent.destination;
    public Vector3 CurrentVelocity => _mover.CurrentVelocity;
    public Quaternion CurrentRotation => _rotator.CurrentRotation;
    
    public bool CanHeal => !_health.IsFullHealth;
    public float HeathPercent => _health.HealthPercent;
    public bool IsDamaged => _health.IsDamaged;
    public bool IsDead => _health.IsDead;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        
        _mover = new AgentMover(_agent, _moveSpeed);
        _rotator = new TransformRotatorDirection(transform);
        _health = new Health(_maxHealth);
    }
    
    private void Update()
    {
        _rotator.Update(Time.deltaTime);
    }
    
    public void SetMoveDirection(Vector3 position) => _mover.SetDestination(position);
    
    public void SetRotateDirection(Vector3 direction) => _rotator.Rotate(direction, _rotateSpeed);
    
    public void TakeDamage(float damage) =>_health.Reduce(damage);
    
    public void Heal(float value) =>_health.Increase(value);
    
    public void SetDamage(bool value) =>_health.SetDamage(value);
    
    public void Stop() => _mover.Stop();

    public void Resume() => _mover.Resume();
}
