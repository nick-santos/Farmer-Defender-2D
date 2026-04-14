using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public Transform holdPoint;

    private ICarryable carriedObject;
    public bool readyToUse = false;
    public LayerMask userTargetLayer;

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
        
        UseItem();
    }

    public bool IsCarrying()
    {
        return carriedObject != null;
    }

    public GameObject MousePositionTrack(LayerMask layer)
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, layer);

            if (hit.collider != null) {
                GameObject selectedObject = hit.collider.gameObject;
                Debug.Log("Selected: " + selectedObject.name);
                return selectedObject;
            }
        }
        return null;
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

        if (carriedObject is not IUsable usable) return;
        
        GameObject clickedObject = MousePositionTrack(userTargetLayer);

        if (clickedObject == null) return;

        if (readyToUse)
        {
            usable.Use(clickedObject);
            readyToUse = false;
            return;
        }

        if (clickedObject == carriedObject.GetTransform().gameObject)
        {
            readyToUse = true;
        }
    }

    void Move()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        rb.MovePosition(transform.position + new Vector3(moveX, moveY, 0) * Time.deltaTime * speed);
    }
    
}
