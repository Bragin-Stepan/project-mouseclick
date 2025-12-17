using UnityEngine;

public partial interface IStoppable
{
    void Stop();
    void Resume();
    bool IsStopped { get; }
}