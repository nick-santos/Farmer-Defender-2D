using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public static WateringCan main;

    public int waterQuantity;
    public int wateringCanMax = 30;

    public bool isNearPlant = false;

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
        if(isNearPlant)
        {
            if(Input.GetKeyDown(KeyCode.E)) 
            {
                SpendWater(10);
                Debug.Log(waterQuantity);
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
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.tag == "Plant")
        {
            isNearPlant = false;
        }
    }
}