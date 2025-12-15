using UnityEngine;
using UnityEngine.AI;

public class AgentMover: IStoppable
{
    private NavMeshAgent _agent;

    public Vector3 CurrentVelocity => _agent.desiredVelocity;
    public bool IsStopped => _agent.isStopped;

    public AgentMover(NavMeshAgent agent, float speed)
    {
        _agent = agent;

        _agent.speed = speed;
        _agent.acceleration = 999;
    }

    public void SetDestination(Vector3 position) => _agent.SetDestination(position);

    public void Stop() => _agent.isStopped = true;
    
    public void Resume() => _agent.isStopped = false;
}
