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
    public float timeWithoutWatering = 10f;
    public bool canBeWatered = true;
    public bool isSeedling = true;

    private Coroutine wateringCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator IndicateWaterIsNeededAfterTime()
    {
        yield return new WaitForSeconds(timeWithoutWatering);

        // icon of water appears
        Debug.Log("WATER");
        canBeWatered = true;

        yield return new WaitForSeconds(5f);

        // plant stop working
        Debug.Log("STOP");
    }

    public void ReceiveWater()
    {
        if(isSeedling)
        {
            sr.sprite = GrownPlant;
            isSeedling = false;
            canBeWatered = false;
            wateringCoroutine = StartCoroutine(IndicateWaterIsNeededAfterTime());
            return;
        }

        if (wateringCoroutine != null)
            StopCoroutine(wateringCoroutine);
        
        canBeWatered = false;
        wateringCoroutine = StartCoroutine(IndicateWaterIsNeededAfterTime());
        
    }
}
