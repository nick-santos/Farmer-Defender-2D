using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class BaseChangeHandler : MonoBehaviour, IInteractable
{
    public WaveManager waveManager;

    public GameObject DemoEndPanel;
    public TMP_Text infoText;

    private bool canMoveToBase = false;
    private bool alreadyMoved = false;
    private bool alreadyInteracted = false;

    void Awake()
    {
        if (waveManager != null)
        {
            waveManager.WavesEnd += OnWavesEnd;
        }
    }

    private void OnWavesEnd(object sender, EventArgs e)
    {
        canMoveToBase = true;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public bool CanInteract()
    {
        return !alreadyMoved;
    }

    public void Interact(Player player)
    {
        if (CanInteract())
        {
            ShowGameObject(infoText.gameObject);
            if (!canMoveToBase)
            {
                // avisar que ainda nao pode
                infoText.text = "Your current house was not fully protected yet";
                Invoke("HideText", 5);
                return;
            }
            if (alreadyInteracted)
            {
                // na segunda vez, se mudar
                infoText.text = "You moved in to this house :)";
                Invoke("HideText", 5);
                alreadyMoved = true;
                ShowGameObject(DemoEndPanel);
                return;
            }

            // perguntar se quer mesmo em UI
            infoText.text = "Press E again to move in to this house :)";
            alreadyInteracted = true;
            Invoke("HideText", 5);
        }
    }

    void Update()
    {
        if (!alreadyMoved) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideGameObject(DemoEndPanel);
        }
    }

    void ShowGameObject(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    void HideGameObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    void HideText()
    {
        infoText.gameObject.SetActive(false);
    }

}
