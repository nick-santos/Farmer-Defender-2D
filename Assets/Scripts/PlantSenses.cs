using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSenses : MonoBehaviour
{
    public ShootingProjectile parent;

    // Start is called before the first frame update
    void Start()
    {
        Transform parentTransform = transform.parent; // entrega um transform
        parent = parentTransform.GetComponent<ShootingProjectile>(); // entrega um componente
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // if(collision.transform.tag == "enemy")
        // {
            if (transform.name == "view")
            {
                parent.isSeeing = true;
            }
        // }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // if(collision.transform.tag == "enemy")
        // {
            if (transform.name == "view")
            {
                parent.isSeeing = false;
            }
        // }
    }
}
