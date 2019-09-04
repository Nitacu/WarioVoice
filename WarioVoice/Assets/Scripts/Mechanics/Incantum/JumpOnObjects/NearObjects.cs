using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearObjects : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CAT")&& !collision.GetComponent<OcelotMovements>().NearToObject)
        {
            collision.GetComponent<OcelotMovements>().stopMove();
            collision.GetComponent<OcelotMovements>().NearToObject = true;
            collision.GetComponent<OcelotMovements>().NearToCollision = GetComponentInParent<EnchantableObj>().gameObject;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CAT"))
        {
            if (!collision.GetComponent<OcelotMovements>().NearToObject || 
                collision.GetComponent<OcelotMovements>().NearToCollision == GetComponentInParent<EnchantableObj>().gameObject)
            {
                if (!collision.GetComponent<OcelotMovements>().InJump)
                {
                    collision.GetComponent<OcelotMovements>().NearToObject = false;
                    collision.GetComponent<OcelotMovements>().NearToCollision = null;
                }
            }
        }
    }
    
}
