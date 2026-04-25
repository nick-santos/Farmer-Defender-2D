using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseRange : MonoBehaviour
{
    public Color defaultColor;
    public Color outOfRangeColor;
    public SpriteRenderer sr;

    public float rangeRadius;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        sr.color = defaultColor;
        rangeRadius = transform.localScale.x / 2;
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float distance = Vector2.Distance(mousePos, transform.position);

        if (distance <= rangeRadius)
        {
            sr.color = defaultColor;
        }
        else
        {
            sr.color = outOfRangeColor;
        }
    }

    // void OnMouseEnter()
    // {
    //     Debug.Log("Mouse Entered");
    //     sr.color = defaultColor;
    // }

    // void OnMouseExit()
    // {
    //     Debug.Log("Mouse Exited");
    //     sr.color = outOfRangeColor;
    // }
}
