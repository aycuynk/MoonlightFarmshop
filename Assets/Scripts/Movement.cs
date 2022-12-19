using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Animator anim;

    private void Update()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, vertical);

        AnimatorMovement(direction);

        transform.position += direction * speed * Time.deltaTime;
    }

    private void AnimatorMovement(Vector3 direction)
    {
        if(anim != null)
        {
            if(direction.magnitude > 0)
            {
                anim.SetBool("isMoving", true);

                anim.SetFloat("horizontal", direction.x);
                anim.SetFloat("vertical", direction.y);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
        }
    }

    public void SetPlayerSpeed(float speed)
    {
        this.speed = speed;
    }
}
