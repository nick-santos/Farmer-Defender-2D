using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldTime : MonoBehaviour
{
    public event EventHandler<TimeSpan> WorldTimeChanged;

    public event EventHandler NightTime;

    [SerializeField] private float dayLength;

    private TimeSpan currentTime;

    private float minuteLength => dayLength / 1440;

    private string nightTime = "19:00";

    void Start()
    {
        StartCoroutine(AddMinute());
    }

    private IEnumerator AddMinute()
    {
        currentTime += TimeSpan.FromMinutes(1);
        WorldTimeChanged?.Invoke(this, currentTime);
        yield return new WaitForSeconds(minuteLength);
        //Debug.Log(currentTime.ToString(@"hh\:mm"));

        if(currentTime.ToString(@"hh\:mm") == nightTime)
        {
            NightTime?.Invoke(this, EventArgs.Empty);
        }
        
        StartCoroutine(AddMinute());
    }
}
