using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterReceiver : MonoBehaviour
{
    [Header("References")]
    public Sprite GrownPlant;
    public SpriteRenderer sr;

    [Header("Attributes")]
    public int waterNeeded = 5;
    float timeWithoutWatering;
    public bool canBeWatered = true;
    public bool isSeedling = true;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveWater()
    {
        if(isSeedling)
        {
            sr.sprite = GrownPlant;
            isSeedling = false;
            return;
        }
        
        
    }
}
