using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WateringCanUI : MonoBehaviour
{
    public Image[] waterPills;

    public int pillAmount = 5;

    public void UpdateUI(int currentWater)
    {
        Debug.Log(currentWater);
        for (int i = 0; i < waterPills.Length; i++)
        {
            if (currentWater <= 0)
            {
                waterPills[i].enabled = false;
            }
            else if (currentWater >= pillAmount)
            {
                currentWater -= pillAmount;
                waterPills[i].enabled = true;
                waterPills[i].fillAmount = 1;
            }
            else
            {
                waterPills[i].enabled = true;
                waterPills[i].fillAmount = (float)currentWater / pillAmount;
                currentWater -= pillAmount;
            }
        }
    }
}