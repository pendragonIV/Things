using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputSystem : MonoBehaviour
{
    
    public float InputHorizontal;
    
    public float InputVertical;

    public bool IsAttack;

    private void Update()
    {
        InputHorizontal = Input.GetAxisRaw("Horizontal");
        InputVertical = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            IsAttack = true;
        }
    }

    public void ResetAttackInput()
    {
        IsAttack = false;
    }

}
