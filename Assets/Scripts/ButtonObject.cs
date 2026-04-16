using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObject : MonoBehaviour, IInteractable
{
    public Transform GetTransform()
    {
        return transform;
    }

    public bool CanInteract()
    {
        return true;
    }

    public void Interact(Player player)
    {
        if (CanInteract())
        {
            Debug.Log("Pressed");
        }
    }
}
