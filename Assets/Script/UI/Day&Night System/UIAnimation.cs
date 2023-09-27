using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{
    public Image image;

    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private float animationSpeed = 0.1f;
    [SerializeField]
    private bool loop = true;
    [SerializeField]
    private Coroutine animationCoroutine;

    public bool isDonePlaying = false;
    [SerializeField]
    private int spriteIndex;

    public void PlayAnimation(int startSprite, int endSprite)
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }
        spriteIndex = startSprite;
        animationCoroutine = StartCoroutine(PlayAnimationCoroutine(startSprite, endSprite));
        
    }

    IEnumerator PlayAnimationCoroutine(int startSprite,int endSprite)
    {
        
        if(spriteIndex > endSprite)
        {
            if (loop)
            {
                spriteIndex = startSprite;
            }
            else
            {
                isDonePlaying = true;
                yield break;
            }
        }
        image.sprite = sprites[spriteIndex];
        spriteIndex++;
        yield return new WaitForSeconds(animationSpeed);
        animationCoroutine = StartCoroutine(PlayAnimationCoroutine(startSprite, endSprite));
    }

}
