using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public Transform holdPoint;

    private ICarryable carriedObject;

    private Rigidbody2D rb;

    float moveX;
    float moveY;
    bool isMoving;

    // public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        // Animation();
        // Attack();
    }

    public bool IsCarrying()
    {
        return carriedObject != null;
    }

    public void PickUp(ICarryable obj)
    {
        if (carriedObject != null) return;

        carriedObject = obj;

        carriedObject.GetTransform().SetParent(holdPoint);
        carriedObject.GetTransform().localPosition = Vector3.zero;

        carriedObject.OnPickup();
    }

    public void Drop()
    {
        if (carriedObject == null) return;

        carriedObject.GetTransform().SetParent(null);

        Vector3 dropPosition = transform.position + transform.right;
        carriedObject.GetTransform().position = dropPosition;

        carriedObject.OnDrop();

        carriedObject = null;
    }

    void Move()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        rb.MovePosition(transform.position + new Vector3(moveX, moveY, 0) * Time.deltaTime * speed);
    }

    // void Animation()
    // {
    //     if (moveX == 0 && moveY == 0)
    //     {
    //         isMoving = false;
    //     }
    //     else
    //     {
    //         isMoving = true;
    //     }

    //     anim.SetBool("isMoving", isMoving);
    //     anim.SetFloat("Horizontal", moveX);
    //     anim.SetFloat("Vertical", moveY);
    // }

    // void Attack()
    // {
    //     if(Input.GetKeyDown(KeyCode.Space))
    //     {
    //         anim.SetTrigger("Attack");
    //     }
    // }
    
}
