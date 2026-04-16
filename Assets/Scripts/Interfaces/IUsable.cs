using UnityEngine;

public interface IUsable
{
    float UseRange { get; }
    void Use(GameObject target);
}
