using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionBuilder : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color blockedColor;
    public GameObject player;

    [SerializeField] private bool canBuild;
    private GameObject plant;
    private Color startColor;
    private float radius = 2f;

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
        
        Vector3 playerPos = player.transform.position;

        Vector3 playerToCursor = mouseWorldPosition - playerPos;
        Vector3 dir = playerToCursor.normalized;
        Vector3 cursorVector = dir * radius;
        Vector3 finalPos;

        if(Vector3.Distance(playerPos, mouseWorldPosition) < radius)
        {
            finalPos = mouseWorldPosition;
        }
        else
        {
            finalPos = playerPos + cursorVector;
        }

        transform.position = finalPos;
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
