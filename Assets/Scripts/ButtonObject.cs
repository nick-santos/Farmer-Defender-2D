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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag != "Projectile")
        {
            Debug.Log("Pressed");
        }
    }
}
