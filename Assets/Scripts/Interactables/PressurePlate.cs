using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PressurePlate : MonoBehaviour
{
    // [Header("Attributes")]
    private int requiredPlants = 4;

    public event EventHandler PressurePlateActivated;

    // [Header("References")]
    // [SerializeField] private Gate gate; // script do portão

    private Animator anim;

    private int currentPlants = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plant"))
        {
            currentPlants++;
            CheckPlate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Plant"))
        {
            currentPlants--;
            CheckPlate();
        }
    }

    private void CheckPlate()
    {
        Debug.Log("Plantas na plataforma: " + currentPlants);

        if (currentPlants >= requiredPlants)
        {
            PressurePlateActivated?.Invoke(this, EventArgs.Empty);
        }

        anim.SetInteger("Weight", currentPlants);
    }
}
