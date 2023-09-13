using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private InventoryItemHolder itemHolder;

    private Vector2 position;

    private void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        itemHolder = GetComponentInChildren<InventoryItemHolder>();
   
    }

    public void setData(Sprite sprite)
    {
        itemHolder.setData(sprite);
    }

    private void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, 
            Input.mousePosition, 
            canvas.worldCamera, 
            out position);
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool value)
    {
        gameObject.SetActive(value);
    }

}
