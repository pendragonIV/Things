using System.Collections;
using System.Collections.Generic;
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

    #endregion

    [field: SerializeField]
    public Vector3 spawnPosition;

    [field: SerializeField]
    public float distanceFromPlayer { get; private set; }

    #region Movement Variables
    //Vector 2 for movement
    public Vector2 movement;
    public Vector2 currentVelocity;
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
        //this.rb.MovePosition(this.transform.position + (GameManager.instance.player.transform.position - this.transform.position).normalized * skullWolfData.moveSpeed * Time.deltaTime);
        this.transform.position = Vector3.MoveTowards(this.transform.position, GameManager.instance.player.transform.position, enemyData.moveSpeed * Time.deltaTime);
    }

    public virtual void MoveToSpawn()
    {

        //this.rb.MovePosition(this.transform.position + spawnPosition * skullWolfData.moveSpeed * Time.deltaTime);

        this.transform.position = Vector3.MoveTowards(this.transform.position, spawnPosition, enemyData.moveSpeed * Time.deltaTime);
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
