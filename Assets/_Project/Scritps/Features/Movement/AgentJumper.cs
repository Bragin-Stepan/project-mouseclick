using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentJumper: IStoppable
{
    private NavMeshAgent _agent;
    private float _speed;

    public Vector3 CurrentVelocity => _agent.desiredVelocity;
    public bool IsStopped => _agent.isStopped;

    public AgentJumper(NavMeshAgent agent, float speed)
    {
        _agent = agent;
        _speed = speed;
    }

    public void SetDestination(Vector3 position) => _agent.SetDestination(position);

    public void Jump(OffMeshLinkData offMeshLinkData)
    {
        
    }

    // private IEnumerator JumpProcess(OffMeshLinkData offMeshLinkData)
    // {
    //     
    // }

    public void Stop() => _agent.isStopped = true;
    
    public void Resume() => _agent.isStopped = false;
}
