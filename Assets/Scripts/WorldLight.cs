using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering.Universal;

public class WorldLight : MonoBehaviour
{
    public WorldTime worldTime;
    public Gradient gradient;
    private Light2D globalLight;

    void Awake()
    {
        globalLight = GetComponent<Light2D>();
        worldTime.WorldTimeChanged += OnWorldTimeChanged;
    }

    private void OnDestroy()
    {
        worldTime.WorldTimeChanged -= OnWorldTimeChanged;
    }

    private void OnWorldTimeChanged(object sender, TimeSpan newTime)
    {
        globalLight.color = gradient.Evaluate(PercentOfDay(newTime));
    }

    private float PercentOfDay(TimeSpan timeSpan)
    {
        return (float)timeSpan.TotalMinutes % 1440 / 1440;
    }
}
