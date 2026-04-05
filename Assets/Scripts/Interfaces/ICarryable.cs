using UnityEngine;

public interface ICarryable
{
    Transform GetTransform();
    void OnPickup();
    void OnDrop();
}
