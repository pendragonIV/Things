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
    public int days;

    public float hour
    {
        get { return timer / 3600f; }
    }

    public float minute
    {
        get { return (timer % 3600f) / 60f; }
    }

    #endregion

    public static DayNightController instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        if(hour > 6 && hour < 18)
        {
            uiAnimation.PlayAnimation(0, 9);
        }
        else
        {
            uiAnimation.PlayAnimation(10, 19);
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
   
        }
        else if(hourInt == 18 && minute == 0)
        {
            uiAnimation.PlayAnimation(10, 19);
     
        }

        if(globalLight != null)
        {
            float value = lightCurve.Evaluate(hour);
            Color color = Color.Lerp(dayLightColor, nightLightColor, value);
            globalLight.color = color;
        }
        
    }

    private void NextDay()
    {
        timer = 0;
        days++;
    }
}
