using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5;
    public bool allowDiagonalMovement = false;
    public LayerMask collisonLayer;
    public LayerMask interactableLayer;

    private Vector2 input;
    private Animator animator;
    private bool isMoving;
    private bool isDialogShown;

    private void Start()
    {
        UIManager.Instance.setCoinsText(InventoryManager.Instance.Coins);
        animator = GetComponent<Animator>();
        UIManager.Instance.onShopClosed += () => {
            isDialogShown = false;
        };
        UIManager.Instance.onShopOpened += () => {
            isDialogShown = true;
        };
        //subscribe to the onDialogDismissed event
        DialogManager.Instance.onDialogDismissed += () =>
        {
            isDialogShown = false;
            UIManager.Instance.ShowitemsShop();
        };
    }
    void Update()
    {
        //Disables character update when the dialog is shown
        if (!isDialogShown)
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
                    animator.SetFloat("MoveY", input.y);

                    //Add the input vector to the current position
                    var targetPos = transform.position;
                    targetPos.x += input.x;
                    targetPos.y += input.y;

                    //allow movement only if the player is moving outside the collision layers
                    if (IsWalkable(targetPos))
                    {
                        StartCoroutine(Move(targetPos));
                    }
                }
            }
            animator.SetBool("isMoving", isMoving);


            Interact();
        }

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

    //Checks if the player is moving outside the collision and interactable layers or not
    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.15f, collisonLayer | interactableLayer) != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("MoveX"), animator.GetFloat("MoveY"));
        var interactablePos = transform.position + facingDir;
        Debug.DrawLine(transform.position, interactablePos, Color.red, 0.5f);

        var collider = Physics2D.OverlapCircle(interactablePos, 0.3f, interactableLayer);
        if (collider)
        {
            if (!isDialogShown)
            {
                UIManager.Instance.ShowXIconHint();
            }
            if (Input.GetKey(KeyCode.X))
            {
                isDialogShown = true;
                UIManager.Instance.HideXIconHint();
                collider.GetComponent<IInteractable>()?.Interact();
            }
        }
        else
        {
            isDialogShown = false;
            UIManager.Instance.HideXIconHint();
        }
    }

    public void equib(ShopItem item) {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = item.icon;
    }
}
