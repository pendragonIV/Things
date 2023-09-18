using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{

    #region Components

    private Animator animator;
    private BoxCollider2D boxCollider2D;

    #endregion

    #region Animations Defination

    private const string IDLE = "Idle";
    private const string BREAK = "Pot_Destroy";

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator.Play(IDLE);

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Break()
    {

        animator.Play(BREAK);
        boxCollider2D.enabled = false;  

    }

    public AnimationClip FindAnimation(string name)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }

        return null;
    }

    private IEnumerator WaitForStopAnimation()
    {
        yield return new WaitForSeconds(FindAnimation(BREAK).length);
        animator.enabled = false;
    }

    private IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(10);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Break();
     
        StartCoroutine(WaitForStopAnimation());
        
        StartCoroutine(WaitForDestroy());
    }
}
