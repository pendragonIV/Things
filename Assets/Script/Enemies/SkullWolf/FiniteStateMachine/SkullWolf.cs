using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkullWolf : MonoBehaviour
{

    public Animator animator;
    
    public SkullWolfStateMachine skullWolfStateMachine;

    public EnemiesDataSO skullWolfData;

    public SpriteRenderer spriteRenderer;

    public Rigidbody2D rb;

    [field: SerializeField]
    public Transform playerDetect { get; private set; }

    [field: SerializeField]
    public Vector3 spawnPosition;

    [field: SerializeField]
    public float distanceFromPlayer { get; private set; }

    #region Movement Variables
    //Vector 2 for movement
    private Vector2 movement;
    public Vector2 currentVelocity;

    #endregion

    #region Define Animation

    public const string SKULL_WOLF_IDLE = "SkullWolf_Idle";
    public const string SKULL_WOLF_ATTACK = "SkullWolf_Attack";
    public const string SKULL_WOLF_DEAD = "SkullWolf_Dead";
    public const string SKULL_WOLF_ATTACKED = "SkullWolf_Attacked";

    public SkullWolfState idleState;
    public SkullWolfState attackState;
    public SkullWolfState deadState;
    public SkullWolfState attackedState;

    #endregion


    private void Awake()
    {
        skullWolfStateMachine = new SkullWolfStateMachine();
        
        idleState = new SkullWolfIdleState(this, skullWolfStateMachine, skullWolfData, SKULL_WOLF_IDLE);
        attackState = new SkullWolfAttackState(this, skullWolfStateMachine, skullWolfData, SKULL_WOLF_ATTACK);
        deadState = new SkullWolfDeadState(this, skullWolfStateMachine, skullWolfData, SKULL_WOLF_DEAD);
        attackedState = new SkullWolfAttackedState(this, skullWolfStateMachine, skullWolfData, SKULL_WOLF_ATTACKED);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        spawnPosition = transform.position;

        skullWolfStateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        skullWolfStateMachine.CurrentState.LogicUpdate();

        currentVelocity = rb.velocity; 
    }

    private void FixedUpdate()
    {
        skullWolfStateMachine.CurrentState.PhysicsUpdate();
    }

    public bool checkPlayerInRange()
    {
        return Physics2D.OverlapCircle(playerDetect.position, skullWolfData.detectRange ,skullWolfData.whatIsPlayer);
    }


    #region Set Movement Functions
    public void SetVelocityX(float inputHorizontal)
    {
        movement.Set(inputHorizontal, currentVelocity.y);
        rb.velocity = movement;
        currentVelocity = movement;
    }

    public void SetVelocityY(float inputVertical)
    {
        movement.Set(currentVelocity.x, inputVertical);
        rb.velocity = movement;
        currentVelocity = movement;
    }

    public void MoveToPlayer()
    {
        //this.rb.MovePosition(this.transform.position + (GameManager.instance.player.transform.position - this.transform.position).normalized * skullWolfData.moveSpeed * Time.deltaTime);
        this.transform.position = Vector3.MoveTowards(this.transform.position, GameManager.instance.player.transform.position, skullWolfData.moveSpeed * Time.deltaTime);
    }

    public void MoveToSpawn()
    {

        //this.rb.MovePosition(this.transform.position + spawnPosition * skullWolfData.moveSpeed * Time.deltaTime);

        this.transform.position = Vector3.MoveTowards(this.transform.position, spawnPosition, skullWolfData.moveSpeed * Time.deltaTime);
    }
    #endregion

    #region Check position functions

    public bool CheckXPlayer()
    {
        if(GameManager.instance.player.transform.position.x > this.transform.position.x)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckXSpawn()
    {
        if (spawnPosition.x > this.transform.position.x)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float DistanceFromPlayer()
    {
        return Vector3.Distance(GameManager.instance.player.transform.position, this.transform.position);
        
    }

    public float DistanceFromSpawn()
    {
        return Vector3.Distance(spawnPosition, this.transform.position);
    }

    #endregion

    #region Flip Functions
    public void HorizontalFlip(float inputHorizontal)
    {
        if (inputHorizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (inputHorizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
    #endregion

    #region Check animation functions
    public bool isAnimationPlaying(string animationName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isAnimationFinished(string animationName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion
}
