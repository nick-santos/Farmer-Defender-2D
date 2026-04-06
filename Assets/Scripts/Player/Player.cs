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

    void Start()
    {
        // anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            UseItem();
        }
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

        var rb = carriedObject.GetTransform().GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = false;
            //rb.linearVelocity = Vector2.zero;
        }

        carriedObject.OnPickup();
    }

    public void Drop()
    {
        if (carriedObject == null) return;

        carriedObject.GetTransform().SetParent(null);

        Vector3 dropPosition = transform.position + transform.right;
        carriedObject.GetTransform().position = dropPosition;

        var rb = carriedObject.GetTransform().GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = true;
            //rb.linearVelocity = Vector2.zero;
        }

        carriedObject.OnDrop();

        carriedObject = null;
    }

    void UseItem()
    {
        if (!IsCarrying()) return;

        if (carriedObject is IUsable usable)
        {
            usable.Use();
        }
    }

    void Move()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        rb.MovePosition(transform.position + new Vector3(moveX, moveY, 0) * Time.deltaTime * speed);
    }
    
}
