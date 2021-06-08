using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5;
    public bool allowDiagonalMovement = false;
    public LayerMask collisonLayer;

    private Vector2 input;
    private bool isMoving;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        //only allows input if the player is not moving
        if (!isMoving)
        {
            //get Movement Directions
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //Disable Diagonal Movement
            if (!allowDiagonalMovement)
            {
                if (input.x != 0) input.y = 0;
            }

            if (input != Vector2.zero)
            {
                //use the input values to animate the player
                animator.SetFloat("MoveX", input.x);
                animator.SetFloat("MoveX", input.x);

                //Add the input vector to the current position
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;
                
                //allow movement only if the player is moving outside the collision layers
                if (isWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }
        animator.SetBool("isMoving", isMoving);
    }

    //Smoothing the Player Movement till reaches the Target Position
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;

    }

    //Checks if the player is moving outside the collision layers or not
    private bool isWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.15f, collisonLayer) != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
