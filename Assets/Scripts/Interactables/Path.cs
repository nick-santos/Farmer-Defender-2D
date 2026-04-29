using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Path : MonoBehaviour
{
    public ButtonObject button;
    private bool active = false;

    public float timeUntillDisabled = 1f;
    private Coroutine pathActivationCoroutine;

    private SpriteRenderer sprite;
    private Collider2D col;

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
        if (pathActivationCoroutine != null)
        {
            StopCoroutine(pathActivationCoroutine);
        }
        pathActivationCoroutine = StartCoroutine(PathActivatedTime());
    }

    private IEnumerator PathActivatedTime()
    {
        EnablePath();
        yield return new WaitForSeconds(timeUntillDisabled);
        DisablePath();
    }

    void EnablePath()
    {
        active = true;
        sprite.enabled = active;
        col.enabled = !active;
        Debug.Log("The path is " + active);
    }

    void DisablePath()
    {
        active = false;
        sprite.enabled = active;
        col.enabled = !active;
        Debug.Log("The path is " + active);
    }
}
