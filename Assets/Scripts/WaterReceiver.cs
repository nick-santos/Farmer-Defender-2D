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

    // Start is called before the first frame update
    void Start()
    {
        srPlant = GetComponent<SpriteRenderer>();
        waterIcon.SetActive(true);
        GetComponent<Shooter>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator IndicateWaterIsNeededAfterTime()
    {
        yield return new WaitForSeconds(timeWithoutWatering);

        // icon of water appears
        //Debug.Log("WATER");
        canBeWatered = true;
        srIcon.sprite = waterIcon1;
        waterIcon.SetActive(true);

        yield return new WaitForSeconds(5f);

        // plant stop working
        //Debug.Log("STOP");
        srIcon.sprite = waterIcon2;
        GetComponent<Shooter>().enabled = false;
    }

    public void ReceiveWater()
    {
        if(isSeedling)
        {
            srPlant.sprite = grownPlant;
            isSeedling = false;
            canBeWatered = false;
            waterIcon.SetActive(false);
            wateringCoroutine = StartCoroutine(IndicateWaterIsNeededAfterTime());
            GetComponent<Shooter>().enabled = true;
            return;
        }

        if (wateringCoroutine != null)
            StopCoroutine(wateringCoroutine);
        
        canBeWatered = false;
        waterIcon.SetActive(false);
        wateringCoroutine = StartCoroutine(IndicateWaterIsNeededAfterTime());
        GetComponent<Shooter>().enabled = true;
        
    }
}
