using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    public Image healthBar;

    private EntityStatus status;
    private float maxHealthPoints;

    void Start()
    {
        status = GetComponent<EntityStatus>();
        maxHealthPoints = status.maxHealthPoints;
    }

    void Update()
    {
        if (status.healthPoints <= 0) 
        {
            Debug.Log("GAME OVER");
        }

        healthBar.fillAmount = (float)status.healthPoints / maxHealthPoints;
    }
}
