using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    #region Components
    public Animator animator;

    public EnemyStateMachine enemyStateMachine;

    public SpriteRenderer spriteRenderer;

    public Rigidbody2D rb;

    public EnemiesDataSO enemyData;

    [field: SerializeField]
    public Transform playerDetect { get; private set; }

    [field: SerializeField]
    public UnitHealth unitHealth { get; set; }

    #endregion

    #region Props
    [field: SerializeField]
    public Vector3 spawnPosition;

    [field: SerializeField]
    public bool isHit;

    [field: SerializeField]
    public bool isDead = false;

    [field: SerializeField]
    public float distanceFromPlayer { get; private set; }

    #endregion

    #region Declare States
    public EnemyState FrontIdleState;
    public EnemyState FrontAttackState;
    public EnemyState FrontDeadState;
    public EnemyState FrontRunState;
    public EnemyState FrontHitState;

    public EnemyState SideIdleState;
    public EnemyState SideAttackState;
    public EnemyState SideDeadState;
    public EnemyState SideHitState;
    #endregion

    void Start()
    {

        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        unitHealth = gameObject.GetComponent<UnitHealth>();

        SetCurrentHealth(enemyData.maxHealth);
        SetCurrentMaxHealth(enemyData.maxHealth);

        enemyStateMachine.Initialize(FrontIdleState);

        spawnPosition = transform.position;
    }

    #region Health Functions

    public void SetCurrentHealth(float currentHealth)
    {
        this.unitHealth.CurrentHealth = currentHealth;
    }
    public void SetCurrentMaxHealth(float currentMaxHealth)
    {
        this.unitHealth.CurrentMaxHealth = currentMaxHealth;
    }

    #endregion

    #region Movement Variables
    //Vector 2 for movement
    public Vector2 movement;
    public Vector2 currentVelocity;
    #endregion

    public virtual bool  checkPlayerInRange()
    {
        return Physics2D.OverlapCircle(playerDetect.position, enemyData.detectRange, enemyData.whatIsPlayer);
    }


    #region Set Movement Functions
    public virtual void SetVelocityX(float inputHorizontal)
    {
        movement.Set(inputHorizontal, currentVelocity.y);
        rb.velocity = movement;
        currentVelocity = movement;
    }

    public virtual void SetVelocityY(float inputVertical)
    {
        movement.Set(currentVelocity.x, inputVertical);
        rb.velocity = movement;
        currentVelocity = movement;
    }

    public virtual void MoveToPlayer()
    {
        currentVelocity = Vector3.MoveTowards(this.transform.position, GameManager.instance.player.transform.position, enemyData.moveSpeed * Time.deltaTime);

        this.rb.MovePosition(currentVelocity);

    }

    public virtual void MoveToSpawn()
    {

        //this.rb.MovePosition(this.transform.position + spawnPosition * skullWolfData.moveSpeed * Time.deltaTime);

        this.transform.position = Vector3.MoveTowards(this.transform.position, spawnPosition, enemyData.moveSpeed * Time.deltaTime);
    }

    #endregion

    #region KnockBack Functions
    public virtual void KnockBack()
    {
        if (isHit)
        {
            Vector2 difference = this.transform.position - GameManager.instance.player.transform.position;
            difference = difference.normalized * enemyData.knockBackForce;
            rb.AddForce(difference, ForceMode2D.Impulse);
        }
    }

    public IEnumerator KnockBackTimer()
    {
        yield return new WaitForSeconds(0.5f);
        SetVelocityX(0);
        SetVelocityY(0);
    }
    #endregion


    #region Hit Functions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerHit"))
        {
            isHit = true;
        }
        else if (collision.gameObject.CompareTag("PlayerHurtBox"))
        {
            var player = GameManager.instance.player.GetComponent<Player>();
            player.isPlayerAttacked = true;
            player.unitHealth.TakeDamage(this.enemyData.attackDamage);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerHit"))
        {
            isHit = false;
        }
        else if (collision.gameObject.CompareTag("PlayerHurtBox"))
        {
            GameManager.instance.player.GetComponent<Player>().isPlayerAttacked = false;
        }
    }
    
    public bool CheckDead()
    {
        if(this.unitHealth.CurrentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    #endregion

    #region Check position functions

    public virtual bool CheckXPlayer()
    {
        if (GameManager.instance.player.transform.position.x > this.transform.position.x)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual bool CheckXSpawn()
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

    public virtual float DistanceFromPlayer()
    {
        return Vector3.Distance(GameManager.instance.player.transform.position, this.transform.position);

    }

    public virtual float DistanceFromSpawn()
    {
        return Vector3.Distance(spawnPosition, this.transform.position);
    }

    #endregion

    #region Flip Functions
    public virtual void HorizontalFlip(float inputHorizontal)
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
    public virtual bool isAnimationPlaying(string animationName)
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

    public virtual bool isAnimationFinished(string animationName)
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
