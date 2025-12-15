using UnityEngine;

public abstract class Controller
{
    private bool _isEnabled;

    public void Update(float deltaTime)
    {
        if (_isEnabled)
            UpdateLogic(deltaTime);
    }

    protected abstract void UpdateLogic(float deltaTime);

    public virtual void Enable() => _isEnabled = true;

    public virtual void Disable() => _isEnabled = false;
}
