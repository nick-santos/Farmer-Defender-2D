using UnityEngine;

public interface IInteractable
{
    Transform GetTransform();
    void Interact(Player player);
    bool CanInteract();
}