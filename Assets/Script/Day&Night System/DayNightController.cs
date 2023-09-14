using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DayNightController : MonoBehaviour
{
    #region Day-Night Cycle Settings
    private bool isNight;
    [SerializeField]
    private Color dayLightColor = Color.white;
    [SerializeField]
    private Color nightLightColor;
    [SerializeField]
    AnimationCurve lightCurve;
    [SerializeField]
    private Light2D globalLight;
    [SerializeField]
    private UIAnimation uiAnimation;

    #endregion



    #region Time
    private const float DAY_LENGTH = 86400f;
    [SerializeField]
    private TMP_Text time;
    [SerializeField]
    private float timer;
    [SerializeField]
    private float timeScale = 60f;
    [SerializeField]
    private int days;
    #endregion

    float hour
    {
        get { return timer / 3600f; }
    }

    float minute
    {
        get { return (timer % 3600f) / 60f; }
    }

    public float timeOfDay;

    private void Awake()
    {
        if(hour > 6 && hour < 18)
        {
            uiAnimation.PlayAnimation(0, 9);
            isNight = false;
        }
        else
        {
            uiAnimation.PlayAnimation(10, 19);
            isNight = true;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime * timeScale;

        int hourInt = Mathf.FloorToInt(this.hour);
        int minute = Mathf.FloorToInt(this.minute);

        time.text = hourInt.ToString() + " : " + minute.ToString();

        if (timer > DAY_LENGTH)
        {
            NextDay();
        }

        if(hourInt == 6 && minute == 0)
        {
            uiAnimation.PlayAnimation(0, 9);
            isNight = false;
        }
        else if(hourInt == 18 && minute == 0)
        {
            uiAnimation.PlayAnimation(10, 19);
            isNight = true;
        }

        float value = lightCurve.Evaluate(hour);
        Color color = Color.Lerp(dayLightColor, nightLightColor, value);
        globalLight.color = color;
    }

    private void NextDay()
    {
        timer = 0;
        days++;
    }
}
