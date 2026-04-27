using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallProp : MonoBehaviour, IInteractable, ICarryable
{
    private bool isCarried = false;

    void Start()
    {
        
    }

    public bool CanInteract()
    {
        return !isCarried;
    }

    public void Interact(Player player)
    {
        if (CanInteract())
        {
            player.PickUp(this);
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void OnPickup()
    {
        isCarried = true;
    }

    public void OnDrop()
    {
        isCarried = false;
    }

    void Update()
    {
        
    }
}
