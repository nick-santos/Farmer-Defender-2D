using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null; //Closest Interactable
    public GameObject interactionIcon;
    public Player player;

    void Start()
    {
        interactionIcon.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (player.IsCarrying())
            {
                player.Drop();
            }
            else
            {
                interactableInRange?.Interact(player);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            interactionIcon.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            interactionIcon.SetActive(false);
        }
    }
}
