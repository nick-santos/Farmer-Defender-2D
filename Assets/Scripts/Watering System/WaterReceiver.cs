using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterReceiver : MonoBehaviour
{
    [Header("References")]
    public Sprite grownPlant;
    public SpriteRenderer srPlant;
    public Sprite waterIcon1;
    public Sprite waterIcon2;
    public SpriteRenderer srIcon;
    public GameObject waterIcon;

    [Header("Attributes")]
    public int waterNeeded = 5;
    public float timeWithoutWatering = 10f;
    public bool canBeWatered = true;
    public bool isSeedling = true;

    private Coroutine wateringCoroutine;
    private PlantObject plant;

    // Start is called before the first frame update
    void Start()
    {
        srPlant = GetComponent<SpriteRenderer>();
        waterIcon.SetActive(true);
        GetComponent<Shooter>().enabled = false;
        plant = GetComponent<PlantObject>();
    }

    private IEnumerator IndicateWaterIsNeededAfterTime()
    {
        canBeWatered = false;
        waterIcon.SetActive(false);
        GetComponent<Shooter>().enabled = true;

        yield return new WaitForSeconds(timeWithoutWatering);

        // icon of water appears
        canBeWatered = true;
        srIcon.sprite = waterIcon1;
        waterIcon.SetActive(true);

        yield return new WaitForSeconds(timeWithoutWatering/2);

        // plant stop working
        srIcon.sprite = waterIcon2;
        GetComponent<Shooter>().enabled = false;
    }

    public void ReceiveWater()
    {
        if(isSeedling)
        {
            if (plant != null) plant.OnGrow();
            //srPlant.sprite = grownPlant;
            isSeedling = false;
            wateringCoroutine = StartCoroutine(IndicateWaterIsNeededAfterTime());
            return;
        }

        if (wateringCoroutine != null)
        {
            StopCoroutine(wateringCoroutine);
        }

        wateringCoroutine = StartCoroutine(IndicateWaterIsNeededAfterTime());
        
        if (plant != null) plant.OnGetWatered();
    }
}
