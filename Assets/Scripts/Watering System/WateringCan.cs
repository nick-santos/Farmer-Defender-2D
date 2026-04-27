using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour, IInteractable, ICarryable, IUsable
{
    public static WateringCan main;

    public int currentWater = 3;
    public int wateringCanMax = 15;
    public float fillCanTimeRate = 1f;

    private int plantNeededWater;
    private bool isCarried = false;

    public float useRange = 2f;
    public float UseRange => useRange;

    //public Transform targetPlant;

    private Coroutine fillCanCoroutine;

    private WateringCanUI UI;
    public Animator anim;

    private void Awake()
    {
        main = this;
    }

    void Start()
    {
        UI = GetComponent<WateringCanUI>();
        UI.UpdateUI(currentWater);
    }

    public bool CanInteract()
    {
        // if not carrying
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
        StopFilling();
        TutorialManager.Instance.CompleteCurrentStep(TutorialStep.PickWateringCan);
    }

    public void OnDrop()
    {
        isCarried = false;
    }

    public void Use(GameObject target)
    {
        if (target == null) return;

        if (target.transform.tag != "Plant") return;

        if(isCarried) // && targetPlant != null && isNearPlant
        {
            if(target.GetComponent<WaterReceiver>().canBeWatered) 
            {
                plantNeededWater = target.GetComponent<WaterReceiver>().waterNeeded;
                
                if(SpendWater(plantNeededWater))
                {
                    UI.UpdateUI(currentWater);
                    target.GetComponent<WaterReceiver>().ReceiveWater();

                    TutorialManager.Instance.CompleteCurrentStep(TutorialStep.WaterPlant);
                }
            }
        }
    }

    public void IncreaseWater(int amount)
    {
        currentWater += amount;
        if(currentWater > wateringCanMax)
        {
            currentWater = wateringCanMax;
        }
        UI.UpdateUI(currentWater);
    }

    private IEnumerator FillWateringCan()
    {
        if (currentWater < wateringCanMax)
        {
            yield return new WaitForSeconds(fillCanTimeRate);
            
            currentWater += 1;
            UI.UpdateUI(currentWater);
            fillCanCoroutine = StartCoroutine(FillWateringCan());
        }
        TutorialManager.Instance.CompleteCurrentStep(TutorialStep.FillWateringCan);
        yield return null;
    }

    void StopFilling()
    {
        if (fillCanCoroutine != null)
        {
            StopCoroutine(fillCanCoroutine);
            fillCanCoroutine = null;
        }
    }

    public bool SpendWater(int amount)
    {
        if(amount <= currentWater)
        {
            currentWater -= amount;
            UI.UpdateUI(currentWater);
            anim.SetTrigger("Water");
            return true;
        }
        else
        {
            Debug.Log("Not enough water");
            anim.SetTrigger("CannotWater");
            return false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Well")
        {
            fillCanCoroutine = StartCoroutine(FillWateringCan());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.tag == "Well")
        {
            StopFilling();
        }
    }
}