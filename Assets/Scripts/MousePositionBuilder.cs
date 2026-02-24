using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionBuilder : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color blockedColor;

    [SerializeField] private bool canBuild;
    private GameObject plant;
    private Color startColor;

    void Start()
    {
        canBuild = true;
        startColor = sr.color;
    }

    void Update()
    {
        MousePositionTrack();

        if(Input.GetMouseButtonDown(0))
        {
            if (canBuild)
            {
                Debug.Log("PLANT");
                BuildPlant();
            }
        }
    }

    void MousePositionTrack()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        transform.position = mouseWorldPosition;
    }

    void BuildPlant()
    {
        //if (plant != null) return;
        GameObject plantToBuild = BuildManager.main.GetSelectedPlant();
        plant = Instantiate(plantToBuild, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        canBuild = false;
        sr.color = blockedColor;
    }

    void OnTriggerExit2D(Collider2D collision)
    {

        canBuild = true;
        sr.color = startColor;
    }
}
