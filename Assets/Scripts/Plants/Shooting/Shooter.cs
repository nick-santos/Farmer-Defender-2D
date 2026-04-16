using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Shooter : MonoBehaviour
{
    [Header("References")]
    public Transform muzzlePoint;
    public GameObject projectile;
    public LayerMask enemyMask;

    [Header("Attribute")]
    public float targetInRange = 5f;
    public float rotationSpeed = 200f;
    public float projPerSec = 1f;

    private Transform target;
    private float timeUntilFire;
    
    private void OnDrawGizmosSelected() 
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetInRange);
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        //RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if(timeUntilFire >= 1f/projPerSec)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        GameObject projObj = Instantiate(projectile, muzzlePoint.position, Quaternion.identity);
        ProjectileBehavior projScript = projObj.GetComponent<ProjectileBehavior>();
        projScript.SetTarget(target);
    }

    private void FindTarget() 
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetInRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetInRange; 
    }

    private void RotateTowardsTarget()
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        //muzzleRotationPoint.rotation = targetRotation;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }


}
