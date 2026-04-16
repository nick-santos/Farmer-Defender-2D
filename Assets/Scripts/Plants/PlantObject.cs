using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantObject : MonoBehaviour, IInteractable, ICarryable, IUsable
{
    private bool isCarried = false;

    public float useRange = 3f;
    public float UseRange => useRange;

    IPlantAbility ability;

    void Start()
    {
        ability = GetComponent<IPlantAbility>();
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

    public void Use(GameObject target)
    {
        if (ability != null)
        {
            ability.Activate(target);
        }
    }

    void OnMouseDown()
    {
        Debug.Log("CLIQUE NA PLANTA");
    }
}
