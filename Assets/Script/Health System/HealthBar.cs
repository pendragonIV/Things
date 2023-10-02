using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Transform followObject;
    [SerializeField]
    private bool isFollow = true;
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private Image fillImage;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private UnitHealth unitHealth;

    private void OnEnable()
    {
        unitHealth.OnChange += SetHealth;
    }

    private void OnDisable()
    {
        unitHealth.OnChange -= SetHealth;
    }


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        fillImage.fillAmount = 1;
    }

    void Update()
    {
        if (followObject != null && isFollow)
        {
            //rectTransform.anchoredPosition = followObject.position;
        }
    }

    public void SetHealth(float health, float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = health;

    }

}
