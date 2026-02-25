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

    [SerializeField] private bool isBlocked;
    private GameObject plant;
    private Color startColor;
    private float radius = 2f;
    private Plant plantToBuild;

    void Start()
    {
        startColor = sr.color;
    }

    void OnEnable()
    {
        plantToBuild = BuildManager.main.GetSelectedPlant();
        sr.enabled = true;
        sr.sprite = plantToBuild.placeholderImage;
        isBlocked = false;
    }

    void Update()
    {
        MousePositionTrack();

        BuildPlant();
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
        //Plant plantToBuild = BuildManager.main.GetSelectedPlant();
        
        if(Input.GetMouseButtonDown(0))
        {
            if (!isBlocked)
            {
                if (Inventory.main.UseItem(plantToBuild.plantType))
                {
                    Debug.Log("PLANT");
                    plant = Instantiate(plantToBuild.prefab, transform.position, Quaternion.identity);
                    
                    this.enabled = false;
                    sr.enabled = false;
                }
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        isBlocked = true;
        sr.color = blockedColor;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isBlocked = false;
        sr.color = startColor;
    }
}
