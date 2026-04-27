using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    public GameObject interactionIcon;
    public Player player;

    List<IInteractable> interactablesInRange = new List<IInteractable>();
    private IInteractable closestInteractable = null;

    void Start()
    {
        interactionIcon.SetActive(false);
    }

    void Update()
    {
        SearchNewTarget();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (player.IsCarrying())
            {
                player.Drop();
            }
            else
            {
                closestInteractable?.Interact(player);
                TutorialManager.Instance.CompleteCurrentStep(TutorialStep.Interact);
            }
        }
    }

    void SearchNewTarget()
    {
        IInteractable closestTarget = null;
        float minDistance = Mathf.Infinity;

        foreach (IInteractable target in interactablesInRange)
        {
            if (target == null || !target.CanInteract()) continue;

            float distance = Vector2.Distance(transform.position, target.GetTransform().position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestTarget = target;
            }
        }

        closestInteractable = closestTarget;

        interactionIcon.SetActive(closestTarget != null); // show or hide the icon
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            if (!interactable.CanInteract()) return;
            
            if (!interactablesInRange.Contains(interactable))
            {
                interactablesInRange.Add(interactable);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            if (interactablesInRange.Contains(interactable))
            {
                interactablesInRange.Remove(interactable);

                if (closestInteractable == interactable)
                {
                    closestInteractable = null;
                }
            }
        }
    }
}
