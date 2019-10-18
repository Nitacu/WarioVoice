using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketControl : MonoBehaviour
{
    [SerializeField] GameObject _smallExplotion;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player"))
        {
            checkRocket();
        }
    }


    public void checkRocket()
    {
        Instantiate(_smallExplotion, transform.position, Quaternion.identity);
        //GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>());

        if (FindObjectOfType<Ammunition>().Amnunition == 0)
        {
            FindObjectOfType<ConfigurationWorms>().lostGame();
        }
        else
        {
            if (FindObjectOfType<ConvertAngles>().TutorialMode)
            {
                FindObjectOfType<ConvertAngles>().TutorialMode = false;
                FindObjectOfType<ConvertAngles>().allowPoint();
            }
            else
            {
                FindObjectOfType<GuideControlWorm>().activeKeepAction();
            }

        }

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        FindObjectOfType<Ammunition>().turnEnemys();
    }
}
