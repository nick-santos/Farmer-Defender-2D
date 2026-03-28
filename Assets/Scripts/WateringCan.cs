using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public static WateringCan main;

    public int waterQuantity;
    public int wateringCanMax = 30;
    public bool isNearPlant = false;
    private int plantNeededWater;

    public Transform targetPlant;

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
            if(Input.GetKeyDown(KeyCode.E)) 
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

    public void IncreaseWater(int amount)
    {
        waterQuantity += amount;
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
    }
}