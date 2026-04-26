using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantObject : MonoBehaviour, IInteractable, ICarryable, IUsable
{
    private bool isCarried = false;

    public float useRange = 3f;
    public float UseRange => useRange;
    public float healingFromWater = 1f;

    private float healthPoints;
    private float maxHealthPoints;

    IPlantAbility ability;
    EntityStatus health;
    Animator anim;

    void Start()
    {
        ability = GetComponent<IPlantAbility>();
        anim = GetComponent<Animator>();
        health = GetComponent<EntityStatus>();

        maxHealthPoints = health.maxHealthPoints;
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

    public void OnGetWatered()
    {
        health.Heal(healingFromWater);
    }

    public void OnGrow()
    {
        if (anim != null) anim.SetTrigger("Grow");
    }

    void Update()
    {
        healthPoints = health.healthPoints;

        if (anim == null) return;

        if (healthPoints / maxHealthPoints > 0.9)
        {
            anim.SetInteger("HealthStage", 0);
        }
        else if (healthPoints / maxHealthPoints > 0.6)
        {
            anim.SetInteger("HealthStage", 1);
        }
        else if (healthPoints / maxHealthPoints > 0.3)
        {
            anim.SetInteger("HealthStage", 2);
        }
        else
        {
            anim.SetInteger("HealthStage", 3);
        }
    }
}
