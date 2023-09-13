using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{

    [SerializeField]
    private ChickenProps chickenProps;

    #region Components
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;

    #endregion

    #region Movement Variables
    //Vector 2 for movement
    private Vector2 movement;
    public Vector2 currentVelocity;

    #endregion

    #region Animations Defination

    public Animator animator;
    private AnimationClip[] clips;
    AnimatorClipInfo[] animatorInfo;
    private string currentAnimation = "";

    private const string CHICKEN_IDLE_FRONT = "Chicken_Front_Idle";
    private const string CHICKEN_IDLE_BACK = "Chicken_Back_Idle";
    private const string CHICKEN_IDLE_LEFT = "Chicken_Left_Idle";
    private const string CHICKEN_IDLE_RIGHT = "Chicken_Right_Idle";

    private const string CHICKEN_PECK_FRONT = "Chicken_Front_Peck";
    private const string CHICKEN_PECK_BACK = "Chicken_Back_Peck";
    private const string CHICKEN_PECK_LEFT = "Chicken_Left_Peck";
    private const string CHICKEN_PECK_RIGHT = "Chicken_Right_Peck";

    private const string CHICKEN_WALK_FRONT = "Chicken_Front_Walk";
    private const string CHICKEN_WALK_BACK = "Chicken_Back_Walk";
    private const string CHICKEN_WALK_LEFT = "Chicken_Left_Walk";
    private const string CHICKEN_WALK_RIGHT = "Chicken_Right_Walk";

    #endregion

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        clips = animator.runtimeAnimatorController.animationClips;

    }

    // Start is called before the first frame update
    void Start()
    {
        
        rb = gameObject.GetComponent<Rigidbody2D>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        StartCoroutine(PlayRandomly());

    }

    // Update is called once per frame
    void Update()
    {
        currentVelocity = rb.velocity;

        animatorInfo = this.animator.GetCurrentAnimatorClipInfo(0);
        currentAnimation = animatorInfo[0].clip.name;
    }

    private void FixedUpdate()
    {
       
        if(currentAnimation.Contains("Walk"))
        {
            if (currentAnimation.Contains("Left"))
            {
                changeVelocityX(-chickenProps.walkSpeed);
            }
            else if (currentAnimation.Contains("Right"))
            {
                changeVelocityX(chickenProps.walkSpeed);
            }
            else if (currentAnimation.Contains("Front"))
            {
                changeVelocityY(-chickenProps.walkSpeed);
            }
            else if (currentAnimation.Contains("Back"))
            {
                changeVelocityY(chickenProps.walkSpeed);
            }
            
        }
        else
        {
            changeVelocityY(0);
            changeVelocityX(0);
        }
        
    }

    private IEnumerator PlayRandomly()
    {
        while (true)
        {
            var randInd = Random.Range(0, clips.Length);

            var randClip = clips[randInd];

            currentAnimation = randClip.name;

            animator.Play(randClip.name);

            // Wait until animation finished than pick the next one "randClip.length"
            yield return new WaitForSeconds(randClip.length);
        }
    }

    private void changeVelocityX(float velocityX)
    {
        movement.Set(velocityX, currentVelocity.y);
        rb.velocity = movement;
        currentVelocity = movement;
    }

    private void changeVelocityY(float velocityY)
    {

        movement.Set(currentVelocity.x, velocityY);
        rb.velocity = movement;
        currentVelocity = movement;

    }
}
