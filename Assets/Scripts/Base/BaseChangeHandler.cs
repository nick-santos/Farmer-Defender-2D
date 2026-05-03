using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseChangeHandler : MonoBehaviour, IInteractable
{
    public WaveManager waveManager;

    public GameObject DemoEndPanel;

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
            if (!canMoveToBase)
            {
                // avisar que ainda não pode
                Debug.Log("Sua base atual ainda não foi protegida completamente");
                return;
            }
            if (alreadyInteracted)
            {
                // na segunda vez, se mudar
                Debug.Log("Se mudou :D");
                alreadyMoved = true;
                ShowPanel();
                return;
            }

            // perguntar se quer mesmo em UI
            Debug.Log("Pressione E novamente para se mudar pra essa base");
            alreadyInteracted = true;
        }
    }

    void Update()
    {
        if (!alreadyMoved) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HidePanel();
        }
    }

    void ShowPanel()
    {
        DemoEndPanel.SetActive(true);
    }

    void HidePanel()
    {
        DemoEndPanel.SetActive(false);
    }

}
