using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gate : MonoBehaviour
{
    public ButtonObject button;
    private bool active = true;

    private SpriteRenderer sprite;
    private Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        if (button != null)
        {
            button.OnButtonPressed += Event_OnButtonPressed;
        }
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    private void Event_OnButtonPressed(object sender, EventArgs e)
    {
        active = !active;
        Debug.Log("The gate is " + active);
        sprite.enabled = active;
        col.enabled = active;
    }

}
