using UnityEngine;

public interface ITargetDetectable
{
    void OnTargetDetected(Transform target);
    void OnTargetLost();
}