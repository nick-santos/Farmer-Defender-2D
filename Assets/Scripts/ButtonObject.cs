using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonObject : MonoBehaviour, IInteractable
{
    public event EventHandler OnButtonPressed;

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
            OnButtonPressed?.Invoke(this, EventArgs.Empty);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Projectile")
        {
            OnButtonPressed?.Invoke(this, EventArgs.Empty);
        }
    }
}
