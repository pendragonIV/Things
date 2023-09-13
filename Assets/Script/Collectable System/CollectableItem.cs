using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [field: SerializeField]
    public ItemSO InventoryItem { get; private set; }
    [field: SerializeField]
    public int Quantity { get; set; } = 1;
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private float AnimationDuration = 1f;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private new BoxCollider2D collider2D;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = InventoryItem.ItemImage;
        collider2D = GetComponent<BoxCollider2D>();
    }

    public void DestroyItem()
    {
        collider2D.enabled = false;
        StartCoroutine(ItemPickupAnimation());

    }

    private IEnumerator ItemPickupAnimation()
    {
        Vector3 startScale = transform.localScale;
        Vector3 targetScale = new Vector3(0, 0, 0);
        float currentTime = 0;

        while (currentTime < AnimationDuration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, currentTime / AnimationDuration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

}

