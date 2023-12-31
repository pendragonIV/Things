using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    #region Status

    public bool isPlayerDead = false;
    public bool isPlayerCanAttack = true;
    public bool isPlayerAttacked = false;

    #endregion

    #region Player Stats

    public float playerMaxHealth;
    public float playerHealth;
    public float playerBaseDamage;

    #endregion

    public StateMachine stateMachine;

    //Reference to the player props
    [field: SerializeField]
    public PlayerProps playerProps;
    [field: SerializeField]
    public UnitHealth unitHealth { get; set; }

    [field: SerializeField]
    public List<Enemy> enemies { get; set; }

    #region Components
    //Components
    public PlayerInputSystem inputSystem;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;
    public SpriteRenderer spriteRenderer;
    #endregion

    #region Animations Defination
    //Reference to the animator of the player
    public Animator animator;

    private const string PLAYER_IDLE_FRONT = "Player_Idle_Front";
    private const string PLAYER_IDLE_BACK = "Player_Idle_Back";
    private const string PLAYER_IDLE_SIDE = "Player_Idle_Side";

    private const string PLAYER_WALK_SIDE = "Player_Walk_Side";
    private const string PLAYER_WALK_BACK = "Player_Walk_Back";
    private const string PLAYER_WALK_FRONT = "Player_Walk_Front";

    private const string PLAYER_SIDE_SLASH = "Player_Side_Slash";
    private const string PLAYER_FRONT_SLASH = "Player_Front_Slash";
    private const string PLAYER_BACK_SLASH = "Player_Back_Slash";

    #endregion

    #region Declare States 
    //Declare object of each state
    public PlayerFrontIdleState frontIdleState;
    public PlayerSideIdleState sideIdleState;
    public PlayerBackIdleState backIdleState;

    public PlayerSideWalkState sideWalkState;
    public PlayerFrontWalkState frontWalkState;
    public PlayerBackWalkState backWalkState;

    public PlayerTopDiagonalWalkState topDiagonalWalkState;
    public PlayerBotDiagonalWalkState bottomDiagonalWalkState;

    public PlayerSideAttackState sideAttackState;
    public PlayerFrontAttackState frontAttackState;
    public PlayerBackAttackState backAttackState;

    #endregion

    #region Movement Variables
    //Vector 2 for movement
    private Vector2 movement;
    public Vector2 currentVelocity;

    #endregion

    private void Awake()
    {
        playerBaseDamage = playerProps.baseDamage;

        stateMachine = new StateMachine();

        List<Enemy> enemies = new List<Enemy>();

        #region Create States
        //Declare each state
        frontIdleState = new PlayerFrontIdleState(this, stateMachine, playerProps, PLAYER_IDLE_FRONT);
        sideIdleState = new PlayerSideIdleState(this, stateMachine, playerProps, PLAYER_IDLE_SIDE);
        backIdleState = new PlayerBackIdleState(this, stateMachine, playerProps, PLAYER_IDLE_BACK);

        sideWalkState = new PlayerSideWalkState(this, stateMachine, playerProps, PLAYER_WALK_SIDE);
        frontWalkState = new PlayerFrontWalkState(this, stateMachine, playerProps, PLAYER_WALK_FRONT);
        backWalkState = new PlayerBackWalkState(this, stateMachine, playerProps, PLAYER_WALK_BACK);

        topDiagonalWalkState = new PlayerTopDiagonalWalkState(this, stateMachine, playerProps, PLAYER_WALK_SIDE);
        bottomDiagonalWalkState = new PlayerBotDiagonalWalkState(this, stateMachine, playerProps, PLAYER_WALK_SIDE);

        sideAttackState = new PlayerSideAttackState(this, stateMachine, playerProps, PLAYER_SIDE_SLASH);
        frontAttackState = new PlayerFrontAttackState(this, stateMachine, playerProps, PLAYER_FRONT_SLASH);
        backAttackState = new PlayerBackAttackState(this, stateMachine, playerProps, PLAYER_BACK_SLASH);
        #endregion

    }


    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        inputSystem = gameObject.GetComponent<PlayerInputSystem>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        unitHealth = gameObject.GetComponent<UnitHealth>();

        //Initialize the state machine with a starting state
        stateMachine.Initialize(frontIdleState);


        SetCurrentHealth(playerProps.maxHealth);
        SetCurrentMaxHealth(playerProps.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        currentVelocity = rb.velocity;
        stateMachine.CurrentState.DoCheckUpdate();
        stateMachine.CurrentState.LogicUpdate();


        //This check will be decible again when have DeadState
        if (CheckDead())
        {
            isPlayerDead = true;
            this.gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    public void SetCurrentHealth(float currentHealth)
    {
        this.unitHealth.CurrentHealth = currentHealth;
    }
    public void SetCurrentMaxHealth(float currentMaxHealth)
    {
        this.unitHealth.CurrentMaxHealth = currentMaxHealth;
    }

    #region Position

    public void SetWorldSpawnPosition()
    {
        Vector3 spawnPosition = this.transform.position;
        spawnPosition.y -= .2f;
        this.playerProps.worldSpawnPosition = spawnPosition;
    }

    #endregion

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
    #endregion

    #region Flip Functions
    public void HorizontalFlip(float inputHorizontal)
    {
        if(inputHorizontal >= 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(inputHorizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
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

    #region Damage Functions

    public void TakeDamage(float damage)
    {
        this.playerHealth -= damage;
    }

    public bool CheckDead()
    {
        if (this.unitHealth.CurrentHealth <= 0)
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
