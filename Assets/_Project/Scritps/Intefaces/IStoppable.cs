using UnityEngine;

public interface IStoppable
{
    void Stop();
    void Resume();
    bool IsStopped { get; }
}