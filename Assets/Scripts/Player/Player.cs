using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Attributes")]
    public float speed;
    public Transform holdPoint;

    [Header("Carry and Use")]
    private ICarryable carriedObject;
    public bool readyToUse = false;
    public LayerMask userTargetLayer;
    private int originalLayer;

    [Header("Range Visuals")]
    public GameObject rangeVisual;

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

    public GameObject MousePositionTrack(LayerMask mask)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, mask);
            if (hit.collider != null) {
                GameObject selectedObject = hit.collider.gameObject;
                Debug.Log("Selected: " + selectedObject.name);
                return selectedObject;
            }
        }
        return null;
    }

    bool IsInRange(Transform carriedObj, GameObject target, float range)
    {
        float distance = Vector2.Distance(carriedObj.position, target.transform.position);
        return distance <= range;
    }
    
    void UpdateRangeVisual(float range)
    {
        rangeVisual.transform.localScale = new Vector3(range * 2, range * 2, 1);
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
            rb.bodyType = RigidbodyType2D.Kinematic;
            //rb.linearVelocity = Vector2.zero;
        }

        GameObject objGO = carriedObject.GetTransform().gameObject;
        originalLayer = objGO.layer;
        objGO.layer = LayerMask.NameToLayer("HeldItem");

        var playerCol = GetComponent<Collider2D>();
        var itemCol = carriedObject.GetTransform().GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(playerCol, itemCol, true);

        carriedObject.OnPickup();
    }

    public void Drop()
    {
        if (carriedObject == null) return;

        if (readyToUse)
        {
            StopUseItem();
        }

        carriedObject.GetTransform().SetParent(null);

        Vector3 dropPosition = transform.position + transform.right;
        carriedObject.GetTransform().position = dropPosition;

        var rb = carriedObject.GetTransform().GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            //rb.linearVelocity = Vector2.zero;
        }

        GameObject objGO = carriedObject.GetTransform().gameObject;
        objGO.layer = originalLayer;

        var playerCol = GetComponent<Collider2D>();
        var itemCol = carriedObject.GetTransform().GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(playerCol, itemCol, false);

        carriedObject.OnDrop();

        carriedObject = null;

        TutorialManager.Instance.CompleteCurrentStep(TutorialStep.DropItem);
    }

    void UseItem()
    {
        if (!IsCarrying()) return;

        if (carriedObject is not IUsable usable) return;

        GameObject clickedObject = MousePositionTrack(userTargetLayer);

        if (Input.GetMouseButtonDown(1) && readyToUse)
        {
            StopUseItem();
        }

        if (clickedObject == null) return;

        if (readyToUse)
        {
            if (IsInRange(carriedObject.GetTransform(), clickedObject, usable.UseRange))
            {
                usable.Use(clickedObject);
            }
            else
            {
                Debug.Log("Out of range");
            }

            StopUseItem();
            return;
        }

        if (clickedObject == carriedObject.GetTransform().gameObject)
        {
            readyToUse = true;

            var col = carriedObject.GetTransform().GetComponent<Collider2D>();
            if (col != null)
            {
                col.enabled = false;
            }

            UpdateRangeVisual(usable.UseRange);
            rangeVisual.SetActive(true);

            TutorialManager.Instance.CompleteCurrentStep(TutorialStep.UseWateringCan);
        }
    }

    void StopUseItem()
    {
        var col = carriedObject.GetTransform().GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = true;
        }
        rangeVisual.SetActive(false);
        readyToUse = false;
        return;
        // if (Input.GetMouseButtonDown(0))
    }

    void Move()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        rb.MovePosition(transform.position + new Vector3(moveX, moveY, 0) * Time.deltaTime * speed);

        if (moveX != 0 || moveY != 0)
        {
            Debug.Log("MOVED");
            TutorialManager.Instance.CompleteCurrentStep(TutorialStep.Move);
        }
    }
    
}
