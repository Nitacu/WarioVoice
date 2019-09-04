using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private bool _needKey = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_needKey)
        {
            if (collision.gameObject.CompareTag("CAT"))
            {
                GetComponent<ChangeScene>().chanceScene();
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("CAT"))
            {
                if (collision.gameObject.GetComponent<OcelotProperties>()._hasObj)
                {
                    GetComponent<Animator>().enabled = true;
                    Invoke("chanceScene",1.1f);
                }
                    
            }
        }

    }

    private void chanceScene()
    {
        GetComponent<ChangeScene>().chanceScene();
    }
}
