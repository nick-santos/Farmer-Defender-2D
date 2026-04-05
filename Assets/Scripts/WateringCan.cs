using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour, IInteractable
{
    public static WateringCan main;

    public int waterQuantity;
    public int wateringCanMax = 30;
    public float fillCanTimeRate = 0.5f;

    private bool isNearPlant = false;
    private int plantNeededWater;

    public Transform targetPlant;

    private Coroutine fillCanCoroutine;

    private void Awake()
    {
        main = this;
    }

    void Start()
    {
        waterQuantity = 10;
    }

    void Update()
    {
        if(isNearPlant && targetPlant != null)
        {
            if(Input.GetKeyDown(KeyCode.E) && targetPlant.GetComponent<WaterReceiver>().canBeWatered) 
            {
                plantNeededWater = targetPlant.GetComponent<WaterReceiver>().waterNeeded;
                
                if(SpendWater(plantNeededWater))
                {
                    Debug.Log(waterQuantity);
                    targetPlant.GetComponent<WaterReceiver>().ReceiveWater();
                }
            }
        }
    }

    public bool CanInteract()
    {
        // if not carrying anything
        return true;
    }

    public void Interact()
    {
        if (CanInteract())
        {
            Debug.Log("interaction :o");
        }
    }

    public void IncreaseWater(int amount)
    {
        waterQuantity += amount;
        if(waterQuantity > wateringCanMax)
        {
            waterQuantity = wateringCanMax;
        }
    }

    private IEnumerator FillWateringCan()
    {
        while (waterQuantity < wateringCanMax)
        {
            yield return new WaitForSeconds(fillCanTimeRate);
            
            waterQuantity += 1;
        }
    }

    public bool SpendWater(int amount)
    {
        if(amount <= waterQuantity)
        {
            waterQuantity -= amount;
            return true;
        }
        else
        {
            Debug.Log("Not enough water");
            return false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Plant")
        {
            isNearPlant = true;
            targetPlant = collision.transform;
        }
        else if(collision.transform.tag == "Well")
        {
            fillCanCoroutine = StartCoroutine(FillWateringCan());
            Debug.Log(waterQuantity);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.tag == "Plant")
        {
            isNearPlant = false;
            if(targetPlant == collision.transform)
            {
                targetPlant = null;
            }
        }
        else if(collision.transform.tag == "Well")
        {
            if (fillCanCoroutine != null)
            {
                StopCoroutine(fillCanCoroutine);
            }
            Debug.Log(waterQuantity);
        }
    }
}