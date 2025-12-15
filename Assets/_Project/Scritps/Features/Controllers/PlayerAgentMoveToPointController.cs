using UnityEngine;
using UnityEngine.AI;

public class PlayerAgentMoveToPointController : Controller
{
    private IDirectionalMovable _movable;
    private IDirectionalRotatable _rotatable;
    private IDirectionalJumpable _jumpable;
    
    private PlayerInput _input;
    
    private Vector3 _targetPosition;
    private readonly LayerMask _movableMask;
    private NavMeshQueryFilter _queryFilter;
    private float _minDistanceToTarget;
    
    private NavMeshPath _pathToTarget = new ();

    public PlayerAgentMoveToPointController(
        IDirectionalMovable movable,
        IDirectionalRotatable rotatable,
        IDirectionalJumpable jumpable,
        PlayerInput input,
        LayerMask movableMask,
        NavMeshQueryFilter queryFilter,
        float minDistanceToTarget = 0.05f
    )
    {
        _movable = movable;
        _jumpable = jumpable;
        _rotatable = rotatable;
        _input = input;
        _queryFilter = queryFilter;
        _movableMask = movableMask;
        _minDistanceToTarget = minDistanceToTarget;
    }
    
    protected override void UpdateLogic(float deltaTime)
    {
        if (_jumpable.IsOnNavMeshLink(out OffMeshLinkData linkData))
        {
            if (_jumpable.InJumpProcess == false)
            {
                _rotatable.SetRotateDirection(linkData.endPos - linkData.startPos);
                _jumpable.Jump(linkData);
            }

            return;
        }
         
        if (_input.OnRightClick)
        {
            if (RaycastUtils.TryGetHitWithMask(Camera.main, _input.PointPosition, _movableMask, out RaycastHit hit))
                _targetPosition = hit.point;
        }
        
        // if (_Character.IsOnNavMeshLink(out OffMeshLinkData linkData))
        // {
        //     if (_Character.InJumpProcess == false)
        //     {
        //         _rotatable.SetRotateDirection(linkData.endPos - linkData.startPos);
        //         _Character.InJumpProcess(linkData);
        //     }
        //
        //     return;
        // }

        if (NavMeshUtils.TryGetPath(_movable.Position, _targetPosition, _queryFilter, _pathToTarget))
        {
            float distanceToTarget = NavMeshUtils.GetPathLength(_pathToTarget);

            if (IsTargetReached(distanceToTarget) == false)
            {
                _movable.Resume();
                _movable.SetMoveDirection(_targetPosition);
                _rotatable.SetRotateDirection(_movable.CurrentVelocity);
                return;
            }
        }

        _movable.Stop();
        _rotatable.Stop();
    }
    
    private bool IsTargetReached(float distanceToTarget) => distanceToTarget <= _minDistanceToTarget;
}
